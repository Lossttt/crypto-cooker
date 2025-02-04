using System.ComponentModel.DataAnnotations;

namespace crypto_app.Core.Entities
{
    public class Country : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(3)]
        public string CountryCode { get; set; }
    }
}