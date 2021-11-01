using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Customer.API.Data
{
    public class CustomerMapping : IEntityTypeConfiguration<Entities.Customer>
    {
        public void Configure(EntityTypeBuilder<Entities.Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasColumnType("VARCHAR(200)");

            builder.OwnsOne(c => c.Cpf, tf =>
            {
                tf.Property(c => c.Number)
                    .IsRequired()
                    .HasMaxLength(Cpf.Length)
                    .HasColumnName("Cpf")
                    .HasColumnType($"VARCHAR({Cpf.Length})");
            });

            builder.OwnsOne(c => c.Email, tf =>
            {
                tf.Property(c => c.Address)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasColumnType($"VARCHAR({Email.MaxLength})");
            });

            builder.HasOne(c => c.Address).WithOne(c => c.Customer);

            builder.ToTable("Customer");
        }
    }
}
