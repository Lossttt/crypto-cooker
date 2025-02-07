using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace crypto_app.Core.Models.Requests.Authentication
{
    public class UserLoginRequest
    {
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}