using HIPO.Core;
using HIPO.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HIPO.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILoginService loginService, ILogger<LoginController> logger)
        {
            _logger = logger;
            _loginService = loginService;
        }

        // [HttpPost]
        // public async Task<IActionResult> Login([FromBody] LoginRequest request)
        // {
        //     try
        //     {
        //         LoginResponse response = await _loginService.ValidateLoginAsync(request);
        //         return Ok(response);
        //     }
        //     catch (UserNotFoundException)
        //     {
        //         return NotFound(new { Message = LoginConstant.UserNotFound });
        //     }
        //     catch (InvalidCredentialsException)
        //     {
        //         return Unauthorized(new { Message = LoginConstant.InvalidCredentials });
        //     }
        //     catch (Exception)
        //     {

        //         return StatusCode(500, new { Message = LoginConstant.LoginError });
        //     }
        // }

        [HttpPost("request-otp")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RequestOtp([FromBody] LoginRequest request)
        {
            try
            {
                await _loginService.SendOtpAsync(request);
                _logger.LogInformation("OTP sent to email: {Email}", request.Email);
                return Ok(new { Message = LoginConstant.OtpSent });
            }
            catch (InvalidCredentialsException exception)
            {
                _logger.LogWarning("Invalid credentials for email: {Email}, {1}", request.Email, exception.Message);
                return Unauthorized(new { Message = LoginConstant.InvalidCredentials });
            }
            catch (Exception exception)
            {
                // Log exception here
                _logger.LogError(exception, LoginConstant.LoginError);
                return StatusCode(500, new { Message = LoginConstant.LoginError + exception.Message });
            }
        }

        [HttpPost("verify-otp")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> VerifyOtp([FromBody] OtpVerificationRequest request)
        {
            try
            {
                var response = await _loginService.ValidateOtpAsync(request);
                _logger.LogInformation("OTP verified for email");
                return Ok(response);
            }
            catch (InvalidCredentialsException exception)
            {
                _logger.LogError(exception.Message);
                return Unauthorized(new { Message = exception.Message });
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, LoginConstant.LoginError);
                return StatusCode(500, new { Message = LoginConstant.LoginError });
            }
        }
    }
}
