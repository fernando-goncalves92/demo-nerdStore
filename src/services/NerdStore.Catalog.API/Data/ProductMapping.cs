using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Catalog.API.Entities;

namespace NerdStore.Catalog.API.Data
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");

            builder.Property(c => c.Description)
                .IsRequired()
                .HasColumnType("VARCHAR(500)");

            builder.Property(c => c.Image)
                .IsRequired()
                .HasColumnType("VARCHAR(250)");

            builder.ToTable("Product");
        }
    }
}
