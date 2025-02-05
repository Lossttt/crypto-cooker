using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crypto_app.Core.Models.Responses.Authentication
{
    public class UserTokenResponse
    {
        public AccessTokenResponse AccessToken { get; set; }
        public string RefreshToken { get; set; }
        // public string[] Roles { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}