using System.ComponentModel.DataAnnotations;

namespace crypto_app.Core.Entities
{
    public class Language : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(5)]
        public string Code { get; set; }
    }
}