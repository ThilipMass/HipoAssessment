using System.ComponentModel.DataAnnotations;

namespace HIPO.Infrastructure;

public class HipoAssessmentOptions
{
    [Key]
    public Guid Id { get; set; } 
    [Required]
    public string Option { get; set; } = string.Empty;
}
