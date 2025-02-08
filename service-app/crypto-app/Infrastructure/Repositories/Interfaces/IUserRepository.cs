using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crypto_app.Core.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace crypto_app.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        ApplicationUser FindAppUserByEmail(string userEmail);
        User FindUserByAppUserId(Guid id);
        Task<bool> CheckPassword(string userName, string password);
        Task<IdentityResult> UpdateAppUser(ApplicationUser appUser);
        void AddRefreshToken(string token, string userId, double daysToExpire);
        Task AddUserAsync(User user);
        Task<bool> GetVerificationStatus(string email);

    }
}