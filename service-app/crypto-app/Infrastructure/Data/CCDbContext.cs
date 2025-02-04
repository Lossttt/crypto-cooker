using System;
using Microsoft.EntityFrameworkCore;
using crypto_app.Core.Entities;
using crypto_app.Core.Entities.Users;

namespace crypto_app.Infrastructure.Data
{
    public class CCDbContext : DbContext
    {
        public CCDbContext(DbContextOptions<CCDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasIndex(e => e.ApplicationAccessCode).IsUnique();
                entity.Property(e => e.ApplicationAccessCode).IsRequired().HasMaxLength(6);
                entity.Property(e => e.UserType).IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.AppIcon).HasMaxLength(256);
                entity.Property(e => e.Theme).IsRequired();
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Code).HasMaxLength(5);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Symbol).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Code).IsRequired().HasMaxLength(3);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CountryCode).IsRequired().HasMaxLength(3);
            });
        }
    }
}