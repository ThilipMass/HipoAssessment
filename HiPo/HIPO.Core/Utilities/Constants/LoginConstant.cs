namespace HIPO.Core;

public static class LoginConstant
{
    //login constant
    public const int OTPLength = 6;
    public const int OTPExpiryMinutes = 5;
    public const string OtpCacheKey = "otp:{0}";

    //login messages
    public const string OtpSent = "OTP sent to your email.";
    //error messages
    public const string InvalidCreds = "Invalid or expired OTP.";
    public const string InvalidCredentials = "Invalid credentials";
    public const string LoginError = "An error occurred while validating the login.";
}
