using System.Net;
using System.Net.Mail;
using HIPO.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HIPO.Core
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> emailOptions, ILogger<EmailService> logger)
        {
            _emailSettings = emailOptions.Value;
            _logger = logger;
        }

        public async Task SendOtpEmailAsync(string recipientEmail, string otp)
        {
            string subject = EmailConstant.OtpEmailSubject;
            string body = string.Format(EmailConstant.OtpEmailBodyTemplate, otp);

            try
            {
                using SmtpClient client = new SmtpClient(_emailSettings.SmtpServer)
                {
                    Port = _emailSettings.Port,
                    Credentials = new NetworkCredential(_emailSettings.FromEmail, _emailSettings.AppPassword),
                    EnableSsl = _emailSettings.EnableSsl
                };

                using var mail = new MailMessage(_emailSettings.FromEmail, recipientEmail, subject, body);
                await client.SendMailAsync(mail);

                _logger.LogInformation("OTP email sent to {Recipient}", recipientEmail);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to send OTP email to {Recipient}", recipientEmail);
                throw new ApplicationException(EmailConstant.EmailSendError);
            }
        }
    }
}
