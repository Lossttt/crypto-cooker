using System.ComponentModel.DataAnnotations;

namespace crypto_app.Core.Entities
{
    public class Currency : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string Symbol { get; set; }

        [Required]
        [StringLength(3)]
        public string Code { get; set; }
    }
}