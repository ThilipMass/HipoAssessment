using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;

namespace HIPO.Infrastructure;
public class AssessmentResponses
{
    [Required]
    [Key]
    public Guid ResponseId { get; set; } = Guid.NewGuid();
    [Required]
    public Guid UserProfileId { get; set; }

     [ForeignKey("UserProfileId")]   
    public UserProfiles UserProfile { get; set; }
    [Required]
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    [Required]
    public string ResponseJSON { get; set; }
    [Required]
    public string MBTIType { get; set; } = string.Empty;
    [Required]
    public int TotalScore { get; set; } = 0;
    [Required]
    public bool Status { get; set; } = true;
   
}
