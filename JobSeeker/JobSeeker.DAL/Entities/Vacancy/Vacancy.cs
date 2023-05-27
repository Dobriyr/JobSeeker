namespace JobSeeker.DAL.Entities.Vacancy;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JobSeeker.DAL.Entities.Site;

[Table("vacancies", Schema = "vacancy")]
public class Vacancy
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int SiteId { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
	[MaxLength(400)]
	public string Link { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
	[MaxLength(100)]
	public string Location { get; set; }
        
    [Required]
	[MaxLength(100)]
	public string Company { get; set; }
    public bool? Remote { get; set; }
    public int? Views { get; set; }
    public int? Responses { get; set; }
    public string? Description { get; set; }
    public Site Site { get; set; }
}
