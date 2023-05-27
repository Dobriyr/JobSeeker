namespace JobSeeker.BLL.DTO.Vacancy;
using JobSeeker.DAL.Entities.Site;

public class VacancyDTO
{
	public int Id { get; set; }
	public int SiteId { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Link { get; set; } = string.Empty;
	public DateTime CreatedDate { get; set; }
	public string Location { get; set; } = string.Empty;
	public string Company { get; set; } = string.Empty;
	public bool? Remote { get; set; }
	public int? Views { get; set; }
	public int? Responses { get; set; }
	public string? Description { get; set; }
	public Site Site { get; set; } = new();
}
