using System.ComponentModel.DataAnnotations;

namespace crypto_app.Core.Models.Requests.PasswordReset
{
    public class VerifyResetTokenRequest
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Token { get; set; }
    }
}