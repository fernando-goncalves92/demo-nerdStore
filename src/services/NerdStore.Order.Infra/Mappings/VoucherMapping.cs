using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Order.Domain.Voucher;

namespace NerdStore.Order.Infra.Mappings
{
    public class VoucherMapping : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Code)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");

            builder.Property(c => c.DiscountPercentage)
                .HasColumnType("DECIMAL(18,2)");

            builder.Property(c => c.DiscountAmount)
                .HasColumnType("DECIMAL(18,2)");

            builder.Property(c => c.AvailableAmount)
                .IsRequired()
                .HasColumnType("INT");

            builder.Property(c => c.DiscountType)
                .IsRequired()
                .HasColumnType("INT");

            builder.Property(c => c.CreateDate)
                .IsRequired()
                .HasColumnType("DATETIME");

            builder.Property(c => c.UsedDate)
                .HasColumnType("DATETIME");

            builder.Property(c => c.ExpirationDate)
                .IsRequired()
                .HasColumnType("DATETIME");

            builder.Property(c => c.IsActive)
                .IsRequired()
                .HasColumnType("BIT");

            builder.Property(c => c.IsUsed)
                .IsRequired()
                .HasColumnType("BIT");

            builder.ToTable("Voucher");
        }
    }
}