namespace JobSeeker.BLL.DTO.Vacancy;

public class VacancyDto
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Link { get; set; } = string.Empty;
	public DateTime CreatedDate { get; set; }
	public string Location { get; set; } = string.Empty;
	public string Company { get; set; } = string.Empty;
	public bool? Remote { get; set; }
	public int? Views { get; set; }
	public int? Responses { get; set; }
	public string? Description { get; set; }
}
