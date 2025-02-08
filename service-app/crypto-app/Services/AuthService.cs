using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using crypto_app.Core.DataTransferObjects;
using crypto_app.Core.Entities.Users;
using crypto_app.Core.Mappers;
using crypto_app.Core.Models.Requests.Authentication;
using crypto_app.Core.Models.Responses.Authentication;
using crypto_app.Infrastructure.Repositories.Interfaces;
using crypto_app.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace crypto_app.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenFactory _tokenFactory;
        private readonly IJwtFactory _jwtFactory;
        private readonly IEmailService _emailService;
        private readonly ILogger<AuthService> _logger;
        private const double EXPIRE_DEFAULT = 5;

        public AuthService(
            IUserRepository userRepository,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenFactory tokenFactory,
            IJwtFactory jwtFactory,
            IEmailService emailService,
            ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenFactory = tokenFactory;
            _jwtFactory = jwtFactory;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<(bool isSuccess, TokenDto response, DateTime? lastLogin)> Authenticate(UserLoginRequest request)
        {
            try
            {
                var appUser = _userRepository.FindAppUserByEmail(request.Email);
                if (appUser == null)
                {
                    return (false, null, null);
                }

                var user = GetUserByAppUserId(appUser.Id);
                if (user == null)
                {
                    return (false, null, null);
                }

                if (await _userRepository.CheckPassword(request.Email, request.Password))
                {
                    var lastLogin = appUser.LastLogin;

                    appUser.LastLogin = DateTime.UtcNow;
                    await _userRepository.UpdateAppUser(appUser);

                    var tokenDto = await GenerateTokenDto(appUser, user);
                    return (true, tokenDto, lastLogin);
                }

                return (false, null, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during authentication.");
                return (false, null, null);
            }
        }

        private async Task<TokenDto> GenerateTokenDto(ApplicationUser appUser, User user)
        {
            // generate refresh token
            var refreshToken = _tokenFactory.GenerateToken();
            _userRepository.AddRefreshToken(refreshToken, appUser.Id.ToString(), EXPIRE_DEFAULT);

            // generate access token
            var accessToken = await _jwtFactory.GenerateEncodedToken(appUser, user);

            return new TokenDto()
            {
                AccessToken = new AccessTokenDto(accessToken, 30),
                RefreshToken = refreshToken,
                UserId = appUser.Id.ToString()
            };
        }

        public async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
        {
            var authClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddMinutes(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public User GetUserByAppUserId(Guid userId)
        {
            User user = _userRepository.FindUserByAppUserId(userId);
            return user;
        }

        public async Task<(bool isSuccess, UserRegistrationResponse response, string? errorMessage)> RegisterUserAsync(UserRegistrationRequest request)
        {
            var appUser = UserMapper.MapToApplicationUser(request);
            var result = await _userManager.CreateAsync(appUser, request.Password);

            if (result.Succeeded)
            {
                var user = UserMapper.MapToUser(request, appUser.Id);
                await _userRepository.AddUserAsync(user);
                var response = UserMapper.MapToUserRegistrationResponse(appUser, user);

                // Generate verification token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                var verificationUrl = $"{_configuration["AppSettings:ClientUrl"]}/verify-email?userId={appUser.Id}&token={WebUtility.UrlEncode(token)}";

                // Send verification email
                await _emailService.SendAccountVerificationEmailAsync(appUser.Email!, verificationUrl, appUser.UserName ?? "User");

                return (true, response, null);
            }
            else
            {
                var errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                return (false, null, errorMessage);
            }
        }

        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return result.Succeeded;
        }
    }
}