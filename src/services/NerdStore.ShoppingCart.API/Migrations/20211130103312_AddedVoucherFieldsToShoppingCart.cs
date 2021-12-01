using Microsoft.EntityFrameworkCore.Migrations;

namespace NerdStore.ShoppingCart.API.Migrations
{
    public partial class AddedVoucherFieldsToShoppingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "ShoppingCart",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "VoucherUsed",
                table: "ShoppingCart",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VoucherCode",
                table: "ShoppingCart",
                type: "VARCHAR(50)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "ShoppingCart",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "ShoppingCart",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiscountType",
                table: "ShoppingCart",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "VoucherUsed",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "VoucherCode",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "DiscountType",
                table: "ShoppingCart");
        }
    }
}
