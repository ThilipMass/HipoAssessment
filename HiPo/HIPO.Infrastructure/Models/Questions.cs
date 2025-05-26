using System.ComponentModel.DataAnnotations;

namespace HIPO.Infrastructure;

public class Questions
{
    [Key]
    public Guid QuestionId { get; set; }
    public string Questiontext { get; set; } = string.Empty;
    public int SequenceNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Status { get; set; }
}
