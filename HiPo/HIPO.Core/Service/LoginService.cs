using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using HIPO.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HIPO.Core
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<LoginService> _logger;
        private readonly EmailSettings _emailSettings;
        private readonly IEmailService _emailService;
        public LoginService(IUnitOfWork unitOfWork, ITokenService tokenService, IMemoryCache memoryCache, ILogger<LoginService> logger, IOptions<EmailSettings> emailOptions, IEmailService emailService)
        {
            _emailService = emailService;
            _emailSettings = emailOptions.Value;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        // public async Task<LoginResponse> ValidateLoginAsync(LoginRequest request)
        // {
        //     HipoUsers userData = await _unitOfWork.LoginRepository.GetUserByAceId(request.ACEID);
        //     if (userData == null)
        //     {
        //         throw new UserNotFoundException(LoginConstant.UserNotFound);
        //     }

        //     if (!ValidateUser(userData, request))
        //     {
        //         throw new InvalidCredentialsException(LoginConstant.InvalidCredentials);
        //     }

        //     string token = _tokenService.GenerateToken(userData.ACEId, userData.EmailId);
        //     return new LoginResponse { Token = token };
        // }

        public async Task SendOtpAsync(LoginRequest request)
        {
            HipoUsers user = await _unitOfWork.LoginRepository.GetUserByAceId(request.ACEID);
            if (!ValidateUser(user, request))
                throw new InvalidCredentialsException(LoginConstant.InvalidCredentials);

            string otp = GenerateSecureOTP(LoginConstant.OTPLength);

            string cacheKey = string.Format(LoginConstant.OtpCacheKey, request.ACEID);
            _memoryCache.Set(cacheKey, otp, TimeSpan.FromMinutes(LoginConstant.OTPExpiryMinutes));

            // Send OTP via Email (SMTP or SendGrid)
            await _emailService.SendOtpEmailAsync(recipientEmail: user.EmailId, otp: otp);
        }

        public async Task<LoginResponse> ValidateOtpAsync(OtpVerificationRequest request)
        {
            string cacheKey = string.Format(LoginConstant.OtpCacheKey, request.ACEID);
            if (!_memoryCache.TryGetValue(cacheKey, out string? cachedOtp) || cachedOtp != request.OTP)
                throw new InvalidCredentialsException(LoginConstant.InvalidCreds);

            HipoUsers user = await _unitOfWork.LoginRepository.GetUserByAceId(request.ACEID);
            string token = _tokenService.GenerateToken(user.ACEId, user.EmailId);

            _memoryCache.Remove(request.ACEID); // clear OTP after use

            _logger.LogInformation($"User {request.ACEID} logged in successfully.");
            return new LoginResponse { Token = token };
        }
        // private async Task SendEmailAsync(string toEmail, string subject, string body)
        // {
        //     var client = new SmtpClient(LoginConstant.SmtpClient)
        //     {
        //         Port = LoginConstant.SmtpPort,
        //         Credentials = new NetworkCredential(LoginConstant.OtpEmailFrom, LoginConstant.AppPass),
        //         EnableSsl = true
        //     };

        //     MailMessage mail = new MailMessage(LoginConstant.OtpEmailFrom, toEmail, subject, body); // Must be a valid email
        //     await client.SendMailAsync(mail);
        //     Console.WriteLine($"Email sent to {toEmail} with subject: {subject} and body: {body}");
        // }

        private bool ValidateUser(HipoUsers userData, LoginRequest request)
        {
            if (userData == null) return false;

            return userData.ACEId == request.ACEID &&
                   userData.EmailId == request.Email;
        }

        private string GenerateSecureOTP(int length = LoginConstant.OTPLength)
        {
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] bytes = new byte[length];
            rng.GetBytes(bytes);
            StringBuilder otp = new StringBuilder(length);
            foreach (byte b in bytes)
            {
                otp.Append(b % 10);
            }
            return otp.ToString();
        }
    }
}
