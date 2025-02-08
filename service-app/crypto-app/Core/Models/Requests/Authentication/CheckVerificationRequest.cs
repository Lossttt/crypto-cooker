using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crypto_app.Core.Models.Requests.Authentication
{
    public class CheckVerificationRequest
    {
        public required string Email { get; set; }
    }
}