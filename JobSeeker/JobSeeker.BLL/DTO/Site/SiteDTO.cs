namespace JobSeeker.BLL.DTO.Site;

using JobSeeker.DAL.Entities.Vacancy;

public class SiteDTO
{
	public int Id { get; set; }
	public string Link { get; set; } = string.Empty;
	List<Vacancy> Vacancies { get; set; } = new();
}

