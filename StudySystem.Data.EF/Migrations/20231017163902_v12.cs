using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudySystem.Data.EF.Migrations
{
    public partial class v12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DistrictsCode",
                table: "AddressUsers",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvincesCode",
                table: "AddressUsers",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WardCode1",
                table: "AddressUsers",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AddressUsers_districts_code",
                table: "AddressUsers",
                column: "districts_code");

            migrationBuilder.CreateIndex(
                name: "IX_AddressUsers_DistrictsCode",
                table: "AddressUsers",
                column: "DistrictsCode");

            migrationBuilder.CreateIndex(
                name: "IX_AddressUsers_province_code",
                table: "AddressUsers",
                column: "province_code");

            migrationBuilder.CreateIndex(
                name: "IX_AddressUsers_ProvincesCode",
                table: "AddressUsers",
                column: "ProvincesCode");

            migrationBuilder.CreateIndex(
                name: "IX_AddressUsers_ward_code",
                table: "AddressUsers",
                column: "ward_code");

            migrationBuilder.CreateIndex(
                name: "IX_AddressUsers_WardCode1",
                table: "AddressUsers",
                column: "WardCode1");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressUsers_districts_districts_code",
                table: "AddressUsers",
                column: "districts_code",
                principalTable: "districts",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressUsers_districts_DistrictsCode",
                table: "AddressUsers",
                column: "DistrictsCode",
                principalTable: "districts",
                principalColumn: "code");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressUsers_provinces_province_code",
                table: "AddressUsers",
                column: "province_code",
                principalTable: "provinces",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressUsers_provinces_ProvincesCode",
                table: "AddressUsers",
                column: "ProvincesCode",
                principalTable: "provinces",
                principalColumn: "code");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressUsers_wards_ward_code",
                table: "AddressUsers",
                column: "ward_code",
                principalTable: "wards",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressUsers_wards_WardCode1",
                table: "AddressUsers",
                column: "WardCode1",
                principalTable: "wards",
                principalColumn: "code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressUsers_districts_districts_code",
                table: "AddressUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressUsers_districts_DistrictsCode",
                table: "AddressUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressUsers_provinces_province_code",
                table: "AddressUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressUsers_provinces_ProvincesCode",
                table: "AddressUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressUsers_wards_ward_code",
                table: "AddressUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressUsers_wards_WardCode1",
                table: "AddressUsers");

            migrationBuilder.DropIndex(
                name: "IX_AddressUsers_districts_code",
                table: "AddressUsers");

            migrationBuilder.DropIndex(
                name: "IX_AddressUsers_DistrictsCode",
                table: "AddressUsers");

            migrationBuilder.DropIndex(
                name: "IX_AddressUsers_province_code",
                table: "AddressUsers");

            migrationBuilder.DropIndex(
                name: "IX_AddressUsers_ProvincesCode",
                table: "AddressUsers");

            migrationBuilder.DropIndex(
                name: "IX_AddressUsers_ward_code",
                table: "AddressUsers");

            migrationBuilder.DropIndex(
                name: "IX_AddressUsers_WardCode1",
                table: "AddressUsers");

            migrationBuilder.DropColumn(
                name: "DistrictsCode",
                table: "AddressUsers");

            migrationBuilder.DropColumn(
                name: "ProvincesCode",
                table: "AddressUsers");

            migrationBuilder.DropColumn(
                name: "WardCode1",
                table: "AddressUsers");
        }
    }
}
