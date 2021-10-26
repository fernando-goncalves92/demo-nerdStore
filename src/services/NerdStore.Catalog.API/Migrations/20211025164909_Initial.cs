using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NerdStore.Catalog.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(500)", nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    Image = table.Column<string>(type: "VARCHAR(250)", nullable: false),
                    StockAmount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
