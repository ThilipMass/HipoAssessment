namespace HIPO.Core;

public class EmailConstant
{
    //Email constatnts
    public const string OtpEmailSubject = "Your One-Time Password (OTP)";
    public const string OtpEmailBodyTemplate = "Your OTP is: {0}";
    //Email error messages
    public const string EmailSendError = "Failed to send OTP email. Please try again later.";
}
