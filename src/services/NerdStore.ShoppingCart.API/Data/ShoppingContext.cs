using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Data.Interfaces;
using NerdStore.ShoppingCart.API.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.ShoppingCart.API.Data
{
    public class ShoppingContext : DbContext, IUnitOfWork
    {
        public ShoppingContext(DbContextOptions<ShoppingContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Entities.ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();

            EnsureNVarcharColumnsWillNotBeCreate(modelBuilder);

            EnsureNotDeleteInCascade(modelBuilder);

            ApplyMappings(modelBuilder);
        }

        private void EnsureNVarcharColumnsWillNotBeCreate(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder
                .Model
                .GetEntityTypes()
                .SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("VARCHAR(100)");
            }
        }

        private void EnsureNotDeleteInCascade(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder
                .Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }
        }

        private void ApplyMappings(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.ShoppingCart>()
                .HasIndex(c => c.CustomerId)
                .HasName("IDX_Customer");

            modelBuilder.Entity<Entities.ShoppingCart>()
                .ToTable("ShoppingCart")
                .HasMany(i => i.Items)
                .WithOne(s => s.ShoppingCart)
                .HasForeignKey(c => c.ShoppingCartId);

            modelBuilder.Entity<Entities.ShoppingCart>()
                .Ignore(c => c.Voucher)
                .OwnsOne(c => c.Voucher, v =>
                {
                    v.Property(vc => vc.Code).HasColumnName("VoucherCode").HasColumnType("VARCHAR(50)");
                    v.Property(vc => vc.DiscountType).HasColumnName("DiscountType");
                    v.Property(vc => vc.DiscountPercentage).HasColumnName("DiscountPercentage");
                    v.Property(vc => vc.DiscountAmount).HasColumnName("DiscountAmount");
                });

            modelBuilder.Entity<ShoppingCartItem>()
                .ToTable("ShoppingCartItem");
        }

        public async Task<bool> CommitAsync()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
