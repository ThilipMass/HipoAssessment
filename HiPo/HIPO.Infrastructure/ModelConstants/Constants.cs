namespace HIPO.Infrastructure;

public static class Constants
{
    //login constants values
    public const int ACEIDMaxLength = 8;
    public const int ACEIDMinLength = 7;
    public const int ADUserNameMaxLength = 30;
    public const int ADUserNameMinLength = 7;
    //login regex
    public const string ACEIDRegex = @"^ACE\d{4,5}$";
    public const string EmailRegex = @"^[a-zA-Z.]+@aspiresys\.com$";
    //login error messages
    public const string ACEIDErrorMessageMaxLength = "ACEID must be only 8 characters long.";
    public const string ACEIDErrorMessage = "ACEID must start with 'ACE' followed by 4 or 5 digits.";
    public const string EmailErrorMessage = "Invalid email id.";
    public const string UserNotFound = "User not found";
    public const string ADUserNameLengthErrorMessage = "AD user name cannot exceed 30 characters.";
    public const string ADUserNameErrorMessage = "AD user name must be in the format 'firstname.lastname' using lowercase letters only.";
    public const string ADUserNameRegex = @"^[a-z]+\.[a-z]+$";
}
