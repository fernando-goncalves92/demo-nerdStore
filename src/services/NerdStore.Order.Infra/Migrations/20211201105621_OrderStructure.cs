using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NerdStore.Order.Infra.Migrations
{
    public partial class OrderStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "MySequenceStartingIn1000",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR MySequenceStartingIn1000"),
                    CustomerId = table.Column<Guid>(nullable: false),
                    VoucherId = table.Column<Guid>(nullable: true),
                    VoucherUsed = table.Column<bool>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    TotalPurchase = table.Column<decimal>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    OrderStatus = table.Column<int>(nullable: false),
                    Address_Id = table.Column<Guid>(nullable: true),
                    Street = table.Column<string>(type: "VARCHAR(250)", nullable: true),
                    Number = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    Complement = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    District = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    ZipCode = table.Column<string>(type: "VARCHAR(20)", nullable: true),
                    City = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    State = table.Column<string>(type: "VARCHAR(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Voucher_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Voucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    ProductName = table.Column<string>(type: "VARCHAR(250)", nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    ProductImage = table.Column<string>(type: "VARCHAR(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_VoucherId",
                table: "Order",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropSequence(
                name: "MySequenceStartingIn1000");
        }
    }
}
