using System.ComponentModel.DataAnnotations;

namespace HIPO.Infrastructure;

public class HipoAssessmentQuestions
{
    [Key]
    public Guid QuestionId { get; set; }
    [Required]
    public string Question { get; set; } = string.Empty;
}
