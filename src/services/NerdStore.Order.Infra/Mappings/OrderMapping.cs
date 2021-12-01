using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NerdStore.Order.Infra.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Domain.Order.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Order.Order> builder)
        {
            builder.HasKey(c => c.Id);
            
            builder.OwnsOne(p => p.Address, a =>
            {
                a.Property(pe => pe.Street).HasColumnName("Street").HasColumnType("VARCHAR(250)");
                a.Property(pe => pe.Number).HasColumnName("Number").HasColumnType("VARCHAR(50)");
                a.Property(pe => pe.Complement).HasColumnName("Complement").HasColumnType("VARCHAR(100)");
                a.Property(pe => pe.District).HasColumnName("District").HasColumnType("VARCHAR(100)");
                a.Property(pe => pe.ZipCode).HasColumnName("ZipCode").HasColumnType("VARCHAR(20)");
                a.Property(pe => pe.City).HasColumnName("City").HasColumnType("VARCHAR(100)");
                a.Property(pe => pe.State).HasColumnName("State").HasColumnType("VARCHAR(20)");
            });

            builder.Property(c => c.Code).HasDefaultValueSql("NEXT VALUE FOR MySequenceStartingIn1000");

            builder.HasMany(c => c.OrderItems)
                .WithOne(c => c.Order)
                .HasForeignKey(c => c.OrderId);

            builder.ToTable("Order");
        }
    }
}
