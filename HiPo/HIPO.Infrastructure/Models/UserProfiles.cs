using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HIPO.Infrastructure;

public class UserProfiles
{
    [Key]
    public Guid ProfileId { get; set; } = Guid.NewGuid();
    [ForeignKey("Id")]
    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public Users User { get; set; }
    public string Name { get; set; } = string.Empty;
    public string BU { get; set; } = string.Empty;
    public string BatchID { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public string Practice { get; set; } = string.Empty;
    public string DeliveryType { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string ReportingManagerACE { get; set; } = string.Empty;
    public string ReportingManager { get; set; } = string.Empty;
    public string ReportingManagerMail { get; set; } = string.Empty;
    public string FinancialYear { get; set; } = string.Empty;
    public Guid AssessmentStatus { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool Status { get; set; } = true;
}
