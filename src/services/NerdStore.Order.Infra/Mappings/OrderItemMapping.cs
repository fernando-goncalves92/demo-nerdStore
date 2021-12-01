using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Order.Domain.Order;

namespace NerdStore.Order.Infra.Mappings
{
    public class OrderItemMapping : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ProductName)
                .IsRequired()
                .HasColumnType("VARCHAR(250)");

           builder.HasOne(c => c.Order)
                .WithMany(c => c.OrderItems);

            builder.ToTable("OrderItem");
        }
    }
}
