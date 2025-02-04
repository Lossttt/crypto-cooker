using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using crypto_app.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace crypto_app.Core.Entities.Users
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        private string _applicationAccessCode;

        [Required]
        [StringLength(6, MinimumLength = 6)] 
        public string ApplicationAccessCode
        {
            get => _applicationAccessCode;
            set => _applicationAccessCode = HashAccessCode(value);
        }

        public ApplicationUserType UserType { get; set; }
        public DateTime? LastLogin { get; set; }

        // TODO: Tokens

        private string HashAccessCode(string accessCode)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            return hasher.HashPassword(this, accessCode);
        }
    }
}