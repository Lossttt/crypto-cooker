using System.ComponentModel.DataAnnotations;


namespace crypto_app.Core.Models.Requests.Authentication
{
    public class UserRegistrationRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string? AdditionalName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        public DateTime? DateOfBirth { get; set; }
        // [Required]
        // public Guid LanguageId { get; set; }

        // [Required]
        // public Guid CurrencyId { get; set; }

        // [Required]
        // public Guid CountryId { get; set; }
    }
}