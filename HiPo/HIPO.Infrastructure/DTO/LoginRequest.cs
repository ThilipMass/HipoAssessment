using System.ComponentModel.DataAnnotations;

namespace HIPO.Infrastructure;

public class LoginRequest
{
    [Required]
    [RegularExpression(Constants.ACEIDRegex, ErrorMessage = Constants.ACEIDErrorMessage)]
    [StringLength(maximumLength: Constants.ACEIDMaxLength, MinimumLength = Constants.ACEIDMinLength, ErrorMessage = Constants.ACEIDErrorMessageMaxLength)]
    public string ACEID { get; set; } = string.Empty;
    [Required]
    // [RegularExpression(Constants.EmailRegex, ErrorMessage = Constants.EmailErrorMessage)]
    public string Email { get; set; } = string.Empty;
}
