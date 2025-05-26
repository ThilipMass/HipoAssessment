using System.ComponentModel.DataAnnotations;

namespace HIPO.Infrastructure
{
    public class Users
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [RegularExpression(Constants.ACEIDRegex, ErrorMessage = Constants.ACEIDErrorMessage)]
        [StringLength(maximumLength: Constants.ACEIDMaxLength, MinimumLength = Constants.ACEIDMinLength, ErrorMessage = Constants.ACEIDErrorMessageMaxLength)]
        public string ACEId { get; set; } = string.Empty;
        [Required]
        [StringLength(maximumLength: Constants.ADUserNameMaxLength, MinimumLength = Constants.ADUserNameMinLength, ErrorMessage = Constants.ADUserNameLengthErrorMessage)]
        [RegularExpression(Constants.ADUserNameRegex, ErrorMessage = Constants.ADUserNameErrorMessage)]
        public string ADUserName { get; set; } = string.Empty;

        [Required]
        public string EmailId { get; set; } = string.Empty;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public bool Status { get; set; } = true;
    }
}