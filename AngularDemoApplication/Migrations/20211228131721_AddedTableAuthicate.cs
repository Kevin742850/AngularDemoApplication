using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyManagementSystem.Migrations
{
    public partial class AddedTableAuthicate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inspections_InspectionTypes_InspectionTypeId",
                table: "Inspections");

           

            migrationBuilder.AlterColumn<int>(
                name: "InspectionTypeId",
                table: "Inspections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Inspections_InspectionTypes_InspectionTypeId",
                table: "Inspections",
                column: "InspectionTypeId",
                principalTable: "InspectionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inspections_InspectionTypes_InspectionTypeId",
                table: "Inspections");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "InspectionTypeId",
                table: "Inspections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

         

            migrationBuilder.AddForeignKey(
                name: "FK_Inspections_InspectionTypes_InspectionTypeId",
                table: "Inspections",
                column: "InspectionTypeId",
                principalTable: "InspectionTypes",
                principalColumn: "Id");
        }
    }
}
