using HtmlAgilityPack;
using JobSeeker.BLL.Dto.Vacancy;
using JobSeeker.BLL.Services.DataConverter;
using JobSeeker.BLL.Services.Parsers.Base;
using System.Text.RegularExpressions;
using System.Web;

namespace JobSeeker.BLL.Services.Parsers
{
    public class DjiniParser : Parser, IParser
    {
		public DjiniParser() : base()
		{
            this._link = "https://djinni.co/jobs/?all-keywords=&any-of-keywords=&exclude-keywords=&primary_keyword=.NET&exp_level=no_exp";
		}

		public async Task<IEnumerable<VacancyDto>> Parse()
        {
            List<VacancyDto> vacancies = new();
			HtmlDocument doc = new();
			try
            {
				// parsing data from link
				try
                {
                    string html = await _client.GetStringAsync(_link);
                    doc.LoadHtml(html);
                }
                catch(Exception)
                {
                    Console.WriteLine("Bad link");
                }

               HtmlNode jobList = doc.DocumentNode.SelectSingleNode("//ul[@class='list-unstyled list-jobs']");

                if (jobList != null && jobList.HasChildNodes)
                {
                    HtmlNodeCollection jobs = jobList.ChildNodes;
                    foreach (HtmlNode job in jobs)
                    {
                        if (job != null && job.HasChildNodes)
                        {
                            RemoveEmptyNodes(job);
                            VacancyDto vacancy = GetVacancy(job);
                            vacancies.Add(vacancy);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return vacancies;
        }

        private static void RemoveEmptyNodes(HtmlNode toClear)
        {
            for (int i = toClear.ChildNodes.Count - 1; i >= 0; i--)
            {
                HtmlNode node = toClear.ChildNodes[i];
                if (node.InnerHtml == null || node.InnerHtml.Trim() == "")
                {
                    toClear.RemoveChild(node);
                }
            }
        }
        private static VacancyDto GetVacancy(HtmlNode job)
        {
            VacancyDto vacancy = new();
            HtmlNodeCollection jobInfo = job.ChildNodes;
            int stepNum = 1;

            foreach (HtmlNode info in jobInfo)
            {
                switch (stepNum)
                {
                    // link name view response date
                    case 1:
                        {
                            GetDjiniHeader(info, vacancy);
                            break;
                        }
                    // description
                    case 2:
                        {
                            GetDjineFooter(info, vacancy);
                            break;
                        }
                    // location companyname
                    case 3:
                        {
                            GetDjiniBody(info, vacancy);
                            break;
                        }
                }
                stepNum++;
            }

            return vacancy;
        }

        private static void GetDjiniHeader(HtmlNode info, VacancyDto vacancy)
        {
            string[] values = info.InnerText.Trim().Replace('\n', ' ')
                         .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            // get link
            Regex regex = new(@"<a\s+class=""profile""\s+href=""(.*?)"".*?>");
            Match match = regex.Match(info.InnerHtml);

            string link = match.Groups[1].Value;

            if (values[0] == "сьогодні" || values[0] == "вчора")
            {
                if (values[0] == "сьогодні")
                {
                    vacancy.CreatedDate = DateTime.Today;
                }
                if (values[0] == "вчора")
                {
                    vacancy.CreatedDate = DateTime.Today.AddDays(-1);
                }

                vacancy.Name = string.Join(' ', values.Skip(3));
            }
            else
            {
                vacancy.CreatedDate = StringDateConverter.GetDjiniDate(values[0] + " " + values[1]);
                vacancy.Name = string.Join(' ', values.Skip(4));
            }

            vacancy.Link = $"https://djinni.co{link}";
        }

        private static void GetDjineFooter(HtmlNode info, VacancyDto vacancy)
        {
            string data = HttpUtility.HtmlDecode(info.InnerText);
            string footer = $"\n{data.Replace("Детальніше", "").Replace('\n', ' ').Trim()}";

            vacancy.Description = footer;
        }

        private static void GetDjiniBody(HtmlNode info, VacancyDto vacancy)
        {
            string location = info.ChildNodes?[1]?.SelectSingleNode(".//span[@class='location-text']")
                       ?.InnerText
                       ?.Trim().Replace('\n', ' ') ?? "No location";

            string text = info.ChildNodes?[1]?.InnerText ?? "";
            bool canBeRemote = text.Contains("Remote") || text.Contains("Віддалено") || text.Contains("віддалено");

            string companyName = info.ChildNodes?[1]?.SelectSingleNode("./div[@class='list-jobs__details__info']")?
                .SelectSingleNode(".//a[contains(@href,'company=')]").InnerText.Trim() ?? "Cant get name";

            vacancy.Location = location;
            vacancy.Company = companyName;
            vacancy.Remote = canBeRemote;
        }
    }
}
