using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudySystem.Data.EF.Migrations
{
    public partial class v8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_administrative_regions_provinces_administrative_region_id",
                table: "administrative_regions");

            migrationBuilder.DropForeignKey(
                name: "FK_administrative_units_districts_administrative_unit_id",
                table: "administrative_units");

            migrationBuilder.DropForeignKey(
                name: "FK_administrative_units_provinces_administrative_unit_id",
                table: "administrative_units");

            migrationBuilder.DropForeignKey(
                name: "FK_administrative_units_wards_administrative_unit_id",
                table: "administrative_units");

            migrationBuilder.DropForeignKey(
                name: "FK_districts_wards_district_code",
                table: "districts");

            migrationBuilder.DropForeignKey(
                name: "FK_provinces_districts_province_code",
                table: "provinces");

            migrationBuilder.DropIndex(
                name: "IX_provinces_province_code",
                table: "provinces");

            migrationBuilder.DropIndex(
                name: "IX_districts_district_code",
                table: "districts");

            migrationBuilder.DropIndex(
                name: "IX_administrative_units_administrative_unit_id",
                table: "administrative_units");

            migrationBuilder.DropIndex(
                name: "IX_administrative_regions_administrative_region_id",
                table: "administrative_regions");

            migrationBuilder.DropColumn(
                name: "province_code",
                table: "provinces");

            migrationBuilder.DropColumn(
                name: "district_code",
                table: "districts");

            migrationBuilder.DropColumn(
                name: "administrative_unit_id",
                table: "administrative_units");

            migrationBuilder.DropColumn(
                name: "administrative_region_id",
                table: "administrative_regions");

            migrationBuilder.AlterColumn<string>(
                name: "district_code",
                table: "wards",
                type: "varchar(20)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "province_code",
                table: "districts",
                type: "varchar(20)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_wards_administrative_unit_id",
                table: "wards",
                column: "administrative_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_wards_district_code",
                table: "wards",
                column: "district_code");

            migrationBuilder.CreateIndex(
                name: "IX_provinces_administrative_region_id",
                table: "provinces",
                column: "administrative_region_id");

            migrationBuilder.CreateIndex(
                name: "IX_provinces_administrative_unit_id",
                table: "provinces",
                column: "administrative_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_districts_administrative_unit_id",
                table: "districts",
                column: "administrative_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_districts_province_code",
                table: "districts",
                column: "province_code");

            migrationBuilder.AddForeignKey(
                name: "administrative_regions.id",
                table: "districts",
                column: "administrative_unit_id",
                principalTable: "administrative_units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "provinces.code",
                table: "districts",
                column: "province_code",
                principalTable: "provinces",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "administrative_regions.id",
                table: "provinces",
                column: "administrative_unit_id",
                principalTable: "administrative_units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "administrative_units.id",
                table: "provinces",
                column: "administrative_region_id",
                principalTable: "administrative_regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "administrative_regions.id",
                table: "wards",
                column: "administrative_unit_id",
                principalTable: "administrative_units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "districts.code",
                table: "wards",
                column: "district_code",
                principalTable: "districts",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "administrative_regions.id",
                table: "districts");

            migrationBuilder.DropForeignKey(
                name: "provinces.code",
                table: "districts");

            migrationBuilder.DropForeignKey(
                name: "administrative_regions.id",
                table: "provinces");

            migrationBuilder.DropForeignKey(
                name: "administrative_units.id",
                table: "provinces");

            migrationBuilder.DropForeignKey(
                name: "administrative_regions.id",
                table: "wards");

            migrationBuilder.DropForeignKey(
                name: "districts.code",
                table: "wards");

            migrationBuilder.DropIndex(
                name: "IX_wards_administrative_unit_id",
                table: "wards");

            migrationBuilder.DropIndex(
                name: "IX_wards_district_code",
                table: "wards");

            migrationBuilder.DropIndex(
                name: "IX_provinces_administrative_region_id",
                table: "provinces");

            migrationBuilder.DropIndex(
                name: "IX_provinces_administrative_unit_id",
                table: "provinces");

            migrationBuilder.DropIndex(
                name: "IX_districts_administrative_unit_id",
                table: "districts");

            migrationBuilder.DropIndex(
                name: "IX_districts_province_code",
                table: "districts");

            migrationBuilder.AlterColumn<int>(
                name: "district_code",
                table: "wards",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)");

            migrationBuilder.AddColumn<string>(
                name: "province_code",
                table: "provinces",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "province_code",
                table: "districts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)");

            migrationBuilder.AddColumn<string>(
                name: "district_code",
                table: "districts",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "administrative_unit_id",
                table: "administrative_units",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "administrative_region_id",
                table: "administrative_regions",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_provinces_province_code",
                table: "provinces",
                column: "province_code");

            migrationBuilder.CreateIndex(
                name: "IX_districts_district_code",
                table: "districts",
                column: "district_code");

            migrationBuilder.CreateIndex(
                name: "IX_administrative_units_administrative_unit_id",
                table: "administrative_units",
                column: "administrative_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_administrative_regions_administrative_region_id",
                table: "administrative_regions",
                column: "administrative_region_id");

            migrationBuilder.AddForeignKey(
                name: "FK_administrative_regions_provinces_administrative_region_id",
                table: "administrative_regions",
                column: "administrative_region_id",
                principalTable: "provinces",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_administrative_units_districts_administrative_unit_id",
                table: "administrative_units",
                column: "administrative_unit_id",
                principalTable: "districts",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_administrative_units_provinces_administrative_unit_id",
                table: "administrative_units",
                column: "administrative_unit_id",
                principalTable: "provinces",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_administrative_units_wards_administrative_unit_id",
                table: "administrative_units",
                column: "administrative_unit_id",
                principalTable: "wards",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_districts_wards_district_code",
                table: "districts",
                column: "district_code",
                principalTable: "wards",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_provinces_districts_province_code",
                table: "provinces",
                column: "province_code",
                principalTable: "districts",
                principalColumn: "code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
