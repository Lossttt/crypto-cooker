using System.ComponentModel.DataAnnotations;

namespace crypto_app.Core.Models.Requests.PasswordReset
{
    public class PasswordResetRequest
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}