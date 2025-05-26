using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIPO.Infrastructure;

public class Cfg_AssessmentStatus
{
    [Key]
    public Guid Id { get; set; }
    [ForeignKey(nameof(Id))]
    public UserProfiles UserProfile { get; set; }
    public string StatusName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool Status { get; set; }
}
