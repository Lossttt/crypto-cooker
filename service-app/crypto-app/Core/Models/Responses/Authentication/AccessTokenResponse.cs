using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crypto_app.Core.Models.Responses.Authentication
{
    public sealed class AccessTokenResponse
    {
        public string Token { get; }
        public int ExpiresIn { get; }

        public AccessTokenResponse(string token, int expiresIn)
        {
            Token = token;
            ExpiresIn = expiresIn;
        }
    }
}