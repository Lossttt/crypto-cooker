using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crypto_app.Config.API;
using crypto_app.Core.Constants;
using crypto_app.Core.Entities.Users;
using crypto_app.Core.Models.Requests.Authentication;
using crypto_app.Core.Models.Requests.PasswordReset;
using crypto_app.Core.Models.Responses.Authentication;
using crypto_app.Core.Models.Responses.User;
using crypto_app.Infrastructure.Repositories.Interfaces;
using crypto_app.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace crypto_app.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserController> _logger;

        public UserController(
            IAuthService authService,
            ITokenService tokenService,
            IEmailService emailService,
            IUserRepository userRepository,
            UserManager<ApplicationUser> userManager,
            ILogger<UserController> logger)
        {
            _authService = authService;
            _tokenService = tokenService;
            _emailService = emailService;
            _userRepository = userRepository;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost(ApiRoutes.Users.Authenticate)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<UserTokenResponse>> Authenticate([FromBody] UserLoginRequest request)
        {
            try
            {
                var userAuthenticated = await _authService.Authenticate(request);

                if (userAuthenticated.isSuccess)
                {
                    var response = new UserTokenResponse
                    {
                        AccessToken = new AccessTokenResponse(userAuthenticated.response.AccessToken.Token, userAuthenticated.response.AccessToken.ExpiresIn),
                        RefreshToken = userAuthenticated.response.RefreshToken,
                        LastLogin = userAuthenticated.lastLogin
                    };

                    return Ok(response);
                }
                else
                {
                    return BadRequest(new AuthFailedResponse { Message = "Username or password is incorrect" });
                }
            }
            catch (ArgumentNullException argEx)
            {
                _logger.LogError(argEx, "Authentication failed: Missing required parameters.");
                return BadRequest(new { Message = "Missing required parameters." });
            }
            catch (Exception ex)
            {
                _logger.LogError(LogEvent.Processing, ex, "General error while authenticating.");
                return StatusCode(500, new { Message = "An internal error occurred while processing your request." });
            }
        }


        [HttpPost(ApiRoutes.Users.Register)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<UserRegistrationResponse>> Register([FromBody] UserRegistrationRequest request)
        {
            try
            {
                var result = await _authService.RegisterUserAsync(request);

                if (result.isSuccess)
                {
                    var userRegistrationResponse = new UserRegistrationResponse
                    {
                        UserId = result.response.UserId,
                        Email = result.response.Email
                    };

                    return CreatedAtAction(nameof(Register), new { id = userRegistrationResponse.UserId }, userRegistrationResponse);

                }
                else
                {
                    return BadRequest(new AuthFailedResponse { Message = result.errorMessage });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(LogEvent.Processing, e, "General error while registering user");
                return BadRequest();
            }
        }

        [HttpPost(ApiRoutes.Users.RequestPasswordReset)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> RequestPasswordReset([FromBody] PasswordResetRequest request)
        {
            try
            {
                var user = _userRepository.FindAppUserByEmail(request.Email);
                if (user == null)
                {
                    return BadRequest(new { Message = "Email not found" });
                }
                var appUser = _userRepository.FindUserByAppUserId(user.Id);
                var appUserFirstName = appUser.FirstName ?? "User";

                if (string.IsNullOrEmpty(appUser.EmailAddress))
                {
                    return BadRequest(new { Message = "User email address is invalid." });
                }

                _logger.LogInformation($"Password reset requested for {request.Email} by {appUserFirstName}");
                var code = _tokenService.GeneratePasswordResetToken(user);
                await _emailService.SendPasswordResetEmailAsync(appUser.EmailAddress!, code, appUserFirstName);

                return Ok(new { Message = "Password reset email has been sent." });
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while requesting password reset.");
                return StatusCode(500, new { Message = "An internal error occurred while processing your request." });
            }
        }

        [HttpPost(ApiRoutes.Users.VerifyResetToken)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetTokenRequest request)
        {
            try
            {
                var isValid = await _tokenService.VerifyPasswordResetTokenAsync(request.Email, request.Token);
                if (!isValid)
                {
                    return BadRequest(new { Message = "Invalid or expired token" });
                }

                return Ok(new { Message = "Token is valid" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while verifying reset token.");
                return StatusCode(500, new { Message = "An internal error occurred while processing your request." });
            }
        }

        [HttpPost(ApiRoutes.Users.ResetPassword)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            try
            {
                var result = await _authService.ResetPasswordAsync(request.Email, request.Token, request.NewPassword);
                if (!result)
                {
                    return BadRequest(new { Message = "Password reset failed" });
                }

                return Ok(new { Message = "Password reset successful" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while resetting password.");
                return StatusCode(500, new { Message = "An internal error occurred while processing your request." });
            }
        }

        [HttpGet(ApiRoutes.Users.VerifyAccount)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyAccount([FromQuery] string userId, [FromQuery] string token)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return BadRequest(new { Message = "Invalid user ID" });
                }

                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return Ok(new { Message = "Email verified successfully" });
                }
                else
                {
                    return BadRequest(new { Message = "Email verification failed" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while verifying email.");
                return StatusCode(500, new { Message = "An internal error occurred while processing your request." });
            }
        }
    }
}