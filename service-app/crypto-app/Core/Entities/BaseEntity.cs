using System.ComponentModel.DataAnnotations.Schema;

namespace crypto_app.Core.Entities
{
    public abstract class BaseEntity
    {
        [Column(TypeName = "datetime")]
        private DateTime? _createdDateTime;

        [Column(TypeName = "datetime")]
        public DateTime? CreatedDateTime
        {
            get => _createdDateTime != null ? DateTime.SpecifyKind((DateTime)_createdDateTime, DateTimeKind.Utc) : (DateTime?)null;
            set => _createdDateTime = value;
        }
        [Column(TypeName = "datetime")]
        private DateTime? _updatedDateTime;

        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDateTime
        {
            get => _updatedDateTime != null ? DateTime.SpecifyKind((DateTime)_updatedDateTime, DateTimeKind.Utc) : (DateTime?)null;
            set => _updatedDateTime = value;
        }

        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}