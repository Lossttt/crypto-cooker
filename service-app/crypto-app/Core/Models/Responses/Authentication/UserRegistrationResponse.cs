using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crypto_app.Core.Models.Responses.Authentication
{
    public class UserRegistrationResponse
    {
        public required string UserId { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}