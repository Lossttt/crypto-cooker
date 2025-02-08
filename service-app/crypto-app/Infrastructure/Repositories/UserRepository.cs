using crypto_app.Core.Entities.Tokens;
using crypto_app.Core.Entities.Users;
using crypto_app.Infrastructure.Data;
using crypto_app.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace crypto_app.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(
            UserManager<ApplicationUser> userManager,
            ILogger<UserRepository> logger,
            CCDbContext _dbContext) : base(_dbContext)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public ApplicationUser FindAppUserById(string id)
        {
            _logger.LogInformation("Finding user with ID: {Id}", id);
            var users = _userManager.Users.ToList();
            _logger.LogInformation("Total users in the system: {Count}", users.Count);

            var user = users.FirstOrDefault(f => f.Id.ToString() == id);
            if (user == null)
            {
                _logger.LogWarning("User with ID: {Id} not found", id);
                throw new Exception("User not found");
            }
            return user;
        }

        public User FindUserByAppUserId(Guid id)
        {
            ApplicationUser appUser = FindAppUserById(id.ToString());
            User user = null;
            if (appUser is not null && appUser.Email.Length > 0)
            {
                user = _dbSet.FirstOrDefault(u => u.EmailAddress == appUser.Email) ?? throw new Exception("User not found");
            }
            return user;
        }

        public async Task<bool> CheckPassword(string userName, string password)
        {
            try
            {
                var appUser = await _userManager.FindByNameAsync(userName);

                if (appUser == null)
                {
                    return false;
                }

                return await _userManager.CheckPasswordAsync(appUser, password);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking password for user: {UserName}", userName);
                return false;
            }
        }


        public ApplicationUser FindAppUserByEmail(string userEmail)
        {
            if (string.IsNullOrEmpty(userEmail))
            {
                throw new ArgumentNullException(nameof(userEmail));
            }
            var user = _userManager.Users.FirstOrDefault(f => f.UserName == userEmail);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }

        public async Task<IdentityResult> UpdateAppUser(ApplicationUser appUser)
        {
            appUser.NormalizedEmail = _userManager.NormalizeEmail(appUser.Email);
            appUser.NormalizedUserName = _userManager.NormalizeName(appUser.UserName);

            return await _userManager.UpdateAsync(appUser);
        }

        public void AddRefreshToken(string token, string userId, double daysToExpire)
        {
            var refreshToken = new RefreshToken(Guid.NewGuid(), token, DateTime.UtcNow.AddDays(daysToExpire), Guid.Parse(userId));
            _dbContext.RefreshTokens.Add(refreshToken);
            _dbContext.SaveChanges();
        }

        public async Task AddUserAsync(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Log or debug the foreign key values
            _logger.LogInformation("Adding user with ApplicationUserId: {ApplicationUserId}",
                user.ApplicationUserId);

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> GetVerificationStatus(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                _logger.LogError("Email is null or empty");
                throw new ArgumentNullException(nameof(email));
            }

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _logger.LogError("User not found with email: {Email}", email);
                throw new Exception("User not found");
            }

            return user?.EmailConfirmed ?? false;
        }
    }
}