using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crypto_app.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendPasswordResetEmailAsync(string email, string token, string firstName);
    }
}