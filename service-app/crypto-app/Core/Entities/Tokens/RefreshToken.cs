using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using crypto_app.Core.Entities.Users;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace crypto_app.Core.Entities.Tokens
{
    [Table("AspNetRefreshTokens")]
    public class RefreshToken : BaseEntity
    {
        private ApplicationUser _applicationUser;
        public RefreshToken(Guid id, string token, DateTime expires, Guid userId)
        {
            Id = id;
            Token = token;
            Expires = expires;
            UserId = userId;
            UpdatedDateTime = DateTime.UtcNow;
        }
        private RefreshToken(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        private ILazyLoader LazyLoader { get; set; }

        [Key]
        public Guid Id { get; set; }
        public string Token { get; private set; }
        public DateTime Expires { get; private set; }
        public Guid UserId { get; private set; }

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser
        {
            get => LazyLoader.Load(this, ref _applicationUser);
            set => _applicationUser = value;
        }
    }
}