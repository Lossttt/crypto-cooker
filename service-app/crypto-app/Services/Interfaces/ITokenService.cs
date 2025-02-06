using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crypto_app.Core.Entities.Users;

namespace crypto_app.Services.Interfaces
{
    public interface ITokenService
    {
        string GeneratePasswordResetToken(ApplicationUser user);
        Task<bool> VerifyPasswordResetTokenAsync(string email, string token);
    }
}