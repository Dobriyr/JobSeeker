namespace JobSeeker.DAL.Entities.Site;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using JobSeeker.DAL.Entities.Vacancy;

[Table("sites", Schema = "site")]
public class Site
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	[Required]
	[MaxLength(100)]
	public string Link { get; set; }
	List<Vacancy> Vacancies { get; set; }
}

