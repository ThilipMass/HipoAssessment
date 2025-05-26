namespace HIPO.Core;

public interface IEmailService
{
    Task SendOtpEmailAsync(string recipientEmail, string otp);
}

