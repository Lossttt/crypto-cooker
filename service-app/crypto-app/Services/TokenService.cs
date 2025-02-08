using System.Collections.Concurrent;
using crypto_app.Core.Entities.Users;
using crypto_app.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace crypto_app.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private static readonly ConcurrentDictionary<string, (string Token, DateTime Expiry)> TokenStore = new();

        public TokenService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public string GeneratePasswordResetToken(ApplicationUser user)
        {
            var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
            var code = new Random().Next(100000, 999999).ToString();
            TokenStore[code] = (token, DateTime.UtcNow.AddMinutes(30));
            return code;
        }

        public async Task<bool> VerifyPasswordResetTokenAsync(string email, string code)
        {
            if (!TokenStore.TryGetValue(code, out var tokenInfo) || DateTime.UtcNow > tokenInfo.Expiry)
            {
                return false;
            }

            var appUser = await _userManager.FindByEmailAsync(email);
            if (appUser == null)
            {
                return false;
            }

            var result = await _userManager.VerifyUserTokenAsync(appUser, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", tokenInfo.Token);
            return result;
        }

        public async Task<bool> ResetPasswordAsync(string email, string code, string newPassword)
        {
            if (!TokenStore.TryGetValue(code, out var tokenInfo) || DateTime.UtcNow > tokenInfo.Expiry)
            {
                return false; // Code expired or invalid
            }

            var appUser = await _userManager.FindByEmailAsync(email);
            if (appUser == null)
            {
                return false;
            }

            var result = await _userManager.ResetPasswordAsync(appUser, tokenInfo.Token, newPassword);
            if (result.Succeeded)
            {
                TokenStore.TryRemove(code, out _);
            }
            return result.Succeeded;
        }
    }
}