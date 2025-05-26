using HIPO.Core;
using HIPO.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HIPO.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("{aceId}")]
        [ProducesResponseType(typeof(HipoUsers), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserByAceId(string aceId)
        {
            if (string.IsNullOrEmpty(aceId))
            {
                _logger.LogWarning("GetUserByAceId was called with an empty or null ACE ID.");
                return BadRequest(new { Message = UserConstants.AceIdError });
            }

            try
            {
                var user = await _userService.GetUserByAceId(aceId);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "User not found for ACE ID: {AceId}", aceId);
                return NotFound(new { Message = string.Format(UserConstants.UserNotFound, aceId) });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching user by ACE ID: {AceId}", aceId);
                return StatusCode(500, new { Message = UserConstants.UnexpectedError });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<HipoUsers>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<HipoUsers>>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();

                if (users == null || users.Count == 0)
                {
                    _logger.LogInformation("No users were found in the database.");
                    return NotFound(new { Message = UserConstants.UserNotFound });
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all users.");
                return StatusCode(500, new { Message = UserConstants.UnexpectedError });
            }
        }
    }
}
