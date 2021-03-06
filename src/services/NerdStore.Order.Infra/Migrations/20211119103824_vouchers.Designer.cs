// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NerdStore.Order.Infra.Data;

namespace NerdStore.Order.Infra.Migrations
{
    [DbContext(typeof(OrderDbContext))]
    [Migration("20211119103824_vouchers")]
    partial class vouchers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NerdStore.Order.Domain.Voucher.Voucher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AvailableAmount")
                        .HasColumnType("INT");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("DATETIME");

                    b.Property<decimal?>("DiscountAmount")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<decimal?>("DiscountPercentage")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<int>("DiscountType")
                        .HasColumnType("INT");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("DATETIME");

                    b.Property<bool>("IsActive")
                        .HasColumnType("BIT");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("BIT");

                    b.Property<DateTime?>("UsedDate")
                        .HasColumnType("DATETIME");

                    b.HasKey("Id");

                    b.ToTable("Voucher");
                });
#pragma warning restore 612, 618
        }
    }
}
