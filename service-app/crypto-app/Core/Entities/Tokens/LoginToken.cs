using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using crypto_app.Core.Entities.Users;

namespace crypto_app.Core.Entities.Tokens
{
public class LoginToken : BaseEntity
    {
        public LoginToken(string appUserId, string token, DateTime expires)
        {
            Id = new Guid();
            AppUserId = appUserId;
            Token = token;
            Expires = expires;
        }

        
        [Key]
        public Guid Id { get; set; }
        [MaxLength(128)]
        public string AppUserId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public string RemoteIpAddress { get; set; }

        [ForeignKey(nameof(AppUserId))]
        public ApplicationUser User { get; set; }
    }
}