using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NerdStore.Customer.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(254)", nullable: true),
                    Cpf = table.Column<string>(type: "VARCHAR(11)", maxLength: 11, nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Street = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    Number = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Complement = table.Column<string>(type: "varchar(250)", nullable: true),
                    District = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    ZipCode = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    City = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    State = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CustomerId",
                table: "Address",
                column: "CustomerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
