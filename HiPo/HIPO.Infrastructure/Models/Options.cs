using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIPO.Infrastructure;

public class Options
{
    [Key]
    public Guid OptionId { get; set; }
    public Guid QuestionId { get; set; }
    [ForeignKey(nameof(QuestionId))]
    public Questions Questions { get; set; }
    public char OptionLabel { get; set; }
    public string OptionText { get; set; } = string.Empty;
    public string MBTIFactor { get; set; } = string.Empty;
    public int Score { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Status { get; set; }
}
