using crypto_app.Core.DataTransferObjects;
using crypto_app.Core.Entities.Users;
using crypto_app.Core.Models.Requests.Authentication;
using crypto_app.Core.Models.Responses.Authentication;

namespace crypto_app.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(bool isSuccess, TokenDto response, DateTime? lastLogin)> Authenticate(UserLoginRequest request);
        Task<string> GenerateJwtTokenAsync(ApplicationUser user);
        Task<(bool isSuccess, UserRegistrationResponse response, string? errorMessage)> RegisterUserAsync(UserRegistrationRequest request);

    }
}