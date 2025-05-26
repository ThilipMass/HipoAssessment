using System.ComponentModel.DataAnnotations;

namespace HIPO.Infrastructure
{
    public class OtpVerificationRequest
    {
        [Required]
        public string ACEID { get; set; } = string.Empty;

        [Required]
        public string OTP { get; set; } = string.Empty;
    }
}