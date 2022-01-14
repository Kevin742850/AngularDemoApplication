using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyManagementSystem.Migrations
{
    public partial class AlterCustomerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Customer");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Customer",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Customer");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Customer",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
