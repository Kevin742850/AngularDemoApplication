using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyManagementSystem.Migrations
{
    public partial class AddNewTableAndColumnsInMedicineTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormId",
                table: "Medicines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StrengthId",
                table: "Medicines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Form",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Form", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Strength",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strength", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_FormId",
                table: "Medicines",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_StrengthId",
                table: "Medicines",
                column: "StrengthId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Form_FormId",
                table: "Medicines",
                column: "FormId",
                principalTable: "Form",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Strength_StrengthId",
                table: "Medicines",
                column: "StrengthId",
                principalTable: "Strength",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Form_FormId",
                table: "Medicines");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Strength_StrengthId",
                table: "Medicines");

            migrationBuilder.DropTable(
                name: "Form");

            migrationBuilder.DropTable(
                name: "Strength");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_FormId",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_StrengthId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "FormId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "StrengthId",
                table: "Medicines");
        }
    }
}
