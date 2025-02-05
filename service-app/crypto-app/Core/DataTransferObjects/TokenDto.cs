using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crypto_app.Core.DataTransferObjects
{
    public class TokenDto
    {
        public AccessTokenDto AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string UserId { get; set; }
        // public List<string> Roles { get; set; }
        public DateTime? PreviousLogin { get; set; }
    }

    public class AccessTokenDto
    {
        public string Token { get; }
        public int ExpiresIn { get; }

        public AccessTokenDto(string token, int expiresIn)
        {
            Token = token;
            ExpiresIn = expiresIn;
        }
    }
}