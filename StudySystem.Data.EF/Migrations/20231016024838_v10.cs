using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudySystem.Data.EF.Migrations
{
    public partial class v10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "administrative_units",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "administrative_regions",
                newName: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "administrative_units",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "administrative_regions",
                newName: "Id");
        }
    }
}
