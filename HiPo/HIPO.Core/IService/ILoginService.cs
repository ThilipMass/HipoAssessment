using HIPO.Infrastructure;

namespace HIPO.Core;

public interface ILoginService
{
    // Task<LoginResponse> ValidateLoginAsync(LoginRequest request);
    // bool ValidateUser(HipoUsers userData, LoginRequest request);
    Task SendOtpAsync(LoginRequest request);
    Task<LoginResponse> ValidateOtpAsync(OtpVerificationRequest request);
}
