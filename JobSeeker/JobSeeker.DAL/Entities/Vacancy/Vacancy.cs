namespace JobSeeker.DAL.Entities.Vacancy;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("vacancies", Schema = "vacancy")]
public class Vacancy
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(400)]
    public string Link { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    [MaxLength(100)]
    public string Location { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Company { get; set; } = string.Empty;
    public bool? Remote { get; set; }
    public int? Views { get; set; }
    public int? Responses { get; set; }
    public string? Description { get; set; }
}
