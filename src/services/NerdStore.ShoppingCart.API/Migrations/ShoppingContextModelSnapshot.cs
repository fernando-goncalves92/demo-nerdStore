﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NerdStore.ShoppingCart.API.Data;

namespace NerdStore.ShoppingCart.API.Migrations
{
    [DbContext(typeof(ShoppingContext))]
    partial class ShoppingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NerdStore.ShoppingCart.API.Entities.ShoppingCart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalPurchase")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("VoucherUsed")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .HasName("IDX_Customer");

                    b.ToTable("ShoppingCart");
                });

            modelBuilder.Entity("NerdStore.ShoppingCart.API.Entities.ShoppingCartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("Name")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ShoppingCartId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("ShoppingCartItem");
                });

            modelBuilder.Entity("NerdStore.ShoppingCart.API.Entities.ShoppingCart", b =>
                {
                    b.OwnsOne("NerdStore.ShoppingCart.API.Entities.Voucher", "Voucher", b1 =>
                        {
                            b1.Property<Guid>("ShoppingCartId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Code")
                                .HasColumnName("VoucherCode")
                                .HasColumnType("VARCHAR(50)");

                            b1.Property<decimal?>("DiscountAmount")
                                .HasColumnName("DiscountAmount")
                                .HasColumnType("decimal(18,2)");

                            b1.Property<decimal?>("DiscountPercentage")
                                .HasColumnName("DiscountPercentage")
                                .HasColumnType("decimal(18,2)");

                            b1.Property<int>("DiscountType")
                                .HasColumnName("DiscountType")
                                .HasColumnType("int");

                            b1.HasKey("ShoppingCartId");

                            b1.ToTable("ShoppingCart");

                            b1.WithOwner()
                                .HasForeignKey("ShoppingCartId");
                        });
                });

            modelBuilder.Entity("NerdStore.ShoppingCart.API.Entities.ShoppingCartItem", b =>
                {
                    b.HasOne("NerdStore.ShoppingCart.API.Entities.ShoppingCart", "ShoppingCart")
                        .WithMany("Items")
                        .HasForeignKey("ShoppingCartId")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
