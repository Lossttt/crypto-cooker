using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crypto_app.Core.Entities.Users;
using crypto_app.Core.Enums;
using crypto_app.Core.Models.Requests.Authentication;
using crypto_app.Core.Models.Responses.Authentication;

namespace crypto_app.Core.Mappers
{
    public static class UserMapper
    {
        public static UserLoginResponse MapToLoginResponse(ApplicationUser user, string token)
        {
            return new UserLoginResponse
            {
                Token = token,
                RefreshToken = user.RefreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(30)
            };
        }

        public static ApplicationUser MapToApplicationUser(UserRegistrationRequest request)
        {
            return new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
                PhoneNumber = request.PhoneNumber,
                ApplicationAccessCode = Guid.NewGuid().ToString().Substring(0, 6),
                UserType = ApplicationUserType.User
            };
        }

        public static User MapToUser(UserRegistrationRequest request, Guid appUserId)
        {
            return new User
            {
                EmailAddress = request.Email,
                PhoneNumber = request.PhoneNumber,
                ApplicationUserId = appUserId,
                // FirstName = request.FirstName,
                // AdditionalName = request.AdditionalName,
                // LastName = request.LastName,
                // LanguageId = request.LanguageId,
                // CurrencyId = request.CurrencyId,
                // CountryId = request.CountryId
            };
        }

        public static UserRegistrationResponse MapToUserRegistrationResponse(ApplicationUser appUser, User user)
        {
            return new UserRegistrationResponse
            {
                UserId = appUser.Id.ToString(),
                Email = appUser.Email!,
            };
        }
    }
}