using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crypto_app.Core.Entities.Users;

namespace crypto_app.Services.Interfaces
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(ApplicationUser appUser, User user);
    }
}