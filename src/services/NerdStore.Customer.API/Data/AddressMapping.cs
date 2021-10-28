using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Customer.API.Entities;

namespace NerdStore.Customer.API.Data
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Street)
                .IsRequired()
                .HasColumnType("VARCHAR(200)");

            builder.Property(c => c.Number)
                .IsRequired()
                .HasColumnType("VARCHAR(50)");

            builder.Property(c => c.ZipCode)
                .IsRequired()
                .HasColumnType("VARCHAR(20)");

            builder.Property(c => c.Complement)
                .HasColumnType("varchar(250)");

            builder.Property(c => c.District)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");

            builder.Property(c => c.City)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");

            builder.Property(c => c.State)
                .IsRequired()
                .HasColumnType("VARCHAR(50)");

            builder.ToTable("Address");
        }
    }
}
