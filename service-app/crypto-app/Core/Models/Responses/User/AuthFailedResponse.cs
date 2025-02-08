using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crypto_app.Core.Models.Responses.User
{
    public class AuthFailedResponse
    {
        public required string Message { get; set; }
    }
}