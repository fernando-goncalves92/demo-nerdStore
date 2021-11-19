using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NerdStore.Order.Infra.Migrations
{
    public partial class vouchers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: true),
                    AvailableAmount = table.Column<int>(type: "INT", nullable: false),
                    DiscountType = table.Column<int>(type: "INT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UsedDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    IsActive = table.Column<bool>(type: "BIT", nullable: false),
                    IsUsed = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Voucher");
        }
    }
}
