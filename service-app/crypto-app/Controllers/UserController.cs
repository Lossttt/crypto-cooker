using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crypto_app.Config.API;
using crypto_app.Core.Constants;
using crypto_app.Core.Models.Requests.Authentication;
using crypto_app.Core.Models.Responses.Authentication;
using crypto_app.Core.Models.Responses.User;
using crypto_app.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace crypto_app.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly ILogger<UserController> _logger;

        public UserController(IAuthService authService, ILogger<UserController> logger)
        {
            _authService = authService;
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
    }
}