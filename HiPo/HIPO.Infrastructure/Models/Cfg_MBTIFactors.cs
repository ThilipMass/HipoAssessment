using System.ComponentModel.DataAnnotations;

namespace HIPO.Infrastructure;

public class Cfg_MBTIFactors
{
    [Key]
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;    
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool Status { get; set; }
}