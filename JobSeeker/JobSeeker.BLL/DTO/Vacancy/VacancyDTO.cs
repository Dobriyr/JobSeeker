namespace JobSeeker.BLL.Dto.Vacancy;

public class VacancyDto
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Link { get; set; } = string.Empty;
	public DateTime CreatedDate { get; set; }
	public string Location { get; set; } = string.Empty;
	public string Company { get; set; } = string.Empty;
	public bool? Remote { get; set; }
	public string? Description { get; set; }

	public override bool Equals(object? obj)
	{
		if (obj is VacancyShortDto)
		{
			return this.Equals(obj as VacancyShortDto);
		}

		return this.Equals(obj as VacancyDto);	
	}
	public override int GetHashCode()
	{
		return HashCode.Combine(Name, Link, CreatedDate);
	}

	private bool Equals(VacancyDto? other)
	{
		if (other == null)
		{
			return false;
		}

		return
			this.Id == other.Id &&
			this.Name == other.Name &&
			this.Link == other.Link &&
			this.CreatedDate == other.CreatedDate &&
			this.Location == other.Location &&
			this.Company == other.Company &&
			this.Remote == other.Remote &&
			this.Description == other.Description;
	}

	private bool Equals(VacancyShortDto? other)
	{
		if (other == null)
		{
			return false;
		}

		return
			this.Name == other.Name &&
			this.Link == other.Link &&
			this.CreatedDate == other.CreatedDate;
		}
}
