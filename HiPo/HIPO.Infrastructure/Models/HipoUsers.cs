using System.ComponentModel.DataAnnotations;

namespace HIPO.Infrastructure;

public class HipoUsers
{
    [Key]
    public Guid Id { get; set; }
    public string ACEId { get; set; } = string.Empty;
    public string ADUserName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string EmailId { get; set; } = string.Empty;
    public string BU { get; set; } = string.Empty;
    public string BatchID { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public string Practice { get; set; } = string.Empty;
    public string DeliveryType { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string ReportingManagerACE { get; set; } = string.Empty;
    public string ReportingManager { get; set; } = string.Empty;
    public string ReportingManagerMail { get; set; } = string.Empty;
}
