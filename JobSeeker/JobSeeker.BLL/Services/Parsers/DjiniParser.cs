using AutoMapper;
using Azure.Core;
using HtmlAgilityPack;
using JobSeeker.BLL.DTO.Vacancy;
using JobSeeker.BLL.Services.DataConverter;
using JobSeeker.BLL.Services.Parsers.Base;
using JobSeeker.DAL.Entities.Vacancy;
using JobSeeker.DAL.Persistence;
using JobSeeker.DAL.Repositories.Interfaces.Base;
using JobSeeker.DAL.Repositories.Realizations.Base;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace JobSeeker.BLL.Services.Parsers
{
    public class DjiniParser : Parser, IParser
    {
		public DjiniParser()
		{
            this._link = "https://djinni.co/jobs/?all-keywords=&any-of-keywords=&exclude-keywords=&primary_keyword=.NET&exp_level=no_exp";
		}

		public List<VacancyDTO> Parse()
        {
            List<VacancyDTO> vacancies = new();
			HtmlDocument doc = new();
			try
            {
				// parsing data from link
				try
                {
                    string html = _client.DownloadString(_link);
                    doc.LoadHtml(html);
                }
                catch(Exception)
                {
                    Console.WriteLine("Bad link");
                }

                HtmlNode jobList = doc.DocumentNode.SelectSingleNode("//ul[@class='list-unstyled list-jobs']");

                //initialize Vacancy list;
                if (jobList != null && jobList.HasChildNodes)
                {
                    HtmlNodeCollection jobs = jobList.ChildNodes;
                    foreach (HtmlNode job in jobs)
                    {
                        if (job != null && job.HasChildNodes)
                        {
                            RemoveEmptyNodes(job);
                            VacancyDTO vacancy = GetVacancy(job);
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

        private void RemoveEmptyNodes(HtmlNode toClear)
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
        private VacancyDTO GetVacancy(HtmlNode job)
        {
            VacancyDTO vacancy = new();
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

        private void GetDjiniHeader(HtmlNode info, VacancyDTO vacancy)
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
                try
                {
                    vacancy.Views = int.Parse(values[1]);
                    vacancy.Responses = int.Parse(values[2]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " on parse views, responses to int");
                }
            }

            else
            {
                vacancy.CreatedDate = StringDateConverter.GetDjiniDate(values[0] + " " + values[1]);
                vacancy.Name = string.Join(' ', values.Skip(4));

                try
                {
                    vacancy.Views = int.Parse(values[2]);
                    vacancy.Responses = int.Parse(values[3]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " on parse views, responses to int");
                }
            }

            vacancy.Link = $"https://djinni.co{link}";
        }

        private void GetDjineFooter(HtmlNode info, VacancyDTO vacancy)
        {
            string data = HttpUtility.HtmlDecode(info.InnerText);
            string footer = $"\n{data.Replace("Детальніше", "").Replace('\n', ' ').Trim()}";

            vacancy.Description = footer;
        }

        private void GetDjiniBody(HtmlNode info, VacancyDTO vacancy)
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
