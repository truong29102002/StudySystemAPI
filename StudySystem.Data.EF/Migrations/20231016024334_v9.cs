using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudySystem.Data.EF.Migrations
{
    public partial class v9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameIndex(
                name: "IX_wards_district_code",
                table: "wards",
                newName: "idx_wards_district");

            migrationBuilder.RenameIndex(
                name: "IX_wards_administrative_unit_id",
                table: "wards",
                newName: "idx_wards_unit");

            migrationBuilder.RenameIndex(
                name: "IX_provinces_administrative_unit_id",
                table: "provinces",
                newName: "idx_provinces_unit");

            migrationBuilder.RenameIndex(
                name: "IX_provinces_administrative_region_id",
                table: "provinces",
                newName: "idx_provinces_region");

            migrationBuilder.RenameIndex(
                name: "IX_districts_province_code",
                table: "districts",
                newName: "idx_districts_province");

            migrationBuilder.RenameIndex(
                name: "IX_districts_administrative_unit_id",
                table: "districts",
                newName: "idx_districts_unit");

            migrationBuilder.AddColumn<int>(
                name: "AdministrativeRegionsId1",
                table: "provinces",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdministrativeUnitsId",
                table: "provinces",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_provinces_AdministrativeRegionsId1",
                table: "provinces",
                column: "AdministrativeRegionsId1");

            migrationBuilder.CreateIndex(
                name: "IX_provinces_AdministrativeUnitsId",
                table: "provinces",
                column: "AdministrativeUnitsId");

            migrationBuilder.AddForeignKey(
                name: "FK_districts_administrative_units_administrative_unit_id",
                table: "districts",
                column: "administrative_unit_id",
                principalTable: "administrative_units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_districts_provinces_province_code",
                table: "districts",
                column: "province_code",
                principalTable: "provinces",
                principalColumn: "code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_provinces_administrative_regions_administrative_region_id",
                table: "provinces",
                column: "administrative_region_id",
                principalTable: "administrative_regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_provinces_administrative_regions_AdministrativeRegionsId1",
                table: "provinces",
                column: "AdministrativeRegionsId1",
                principalTable: "administrative_regions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_provinces_administrative_units_administrative_unit_id",
                table: "provinces",
                column: "administrative_unit_id",
                principalTable: "administrative_units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_provinces_administrative_units_AdministrativeUnitsId",
                table: "provinces",
                column: "AdministrativeUnitsId",
                principalTable: "administrative_units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_wards_administrative_units_administrative_unit_id",
                table: "wards",
                column: "administrative_unit_id",
                principalTable: "administrative_units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_wards_districts_district_code",
                table: "wards",
                column: "district_code",
                principalTable: "districts",
                principalColumn: "code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_districts_administrative_units_administrative_unit_id",
                table: "districts");

            migrationBuilder.DropForeignKey(
                name: "FK_districts_provinces_province_code",
                table: "districts");

            migrationBuilder.DropForeignKey(
                name: "FK_provinces_administrative_regions_administrative_region_id",
                table: "provinces");

            migrationBuilder.DropForeignKey(
                name: "FK_provinces_administrative_regions_AdministrativeRegionsId1",
                table: "provinces");

            migrationBuilder.DropForeignKey(
                name: "FK_provinces_administrative_units_administrative_unit_id",
                table: "provinces");

            migrationBuilder.DropForeignKey(
                name: "FK_provinces_administrative_units_AdministrativeUnitsId",
                table: "provinces");

            migrationBuilder.DropForeignKey(
                name: "FK_wards_administrative_units_administrative_unit_id",
                table: "wards");

            migrationBuilder.DropForeignKey(
                name: "FK_wards_districts_district_code",
                table: "wards");

            migrationBuilder.DropIndex(
                name: "IX_provinces_AdministrativeRegionsId1",
                table: "provinces");

            migrationBuilder.DropIndex(
                name: "IX_provinces_AdministrativeUnitsId",
                table: "provinces");

            migrationBuilder.DropColumn(
                name: "AdministrativeRegionsId1",
                table: "provinces");

            migrationBuilder.DropColumn(
                name: "AdministrativeUnitsId",
                table: "provinces");

            migrationBuilder.RenameIndex(
                name: "idx_wards_unit",
                table: "wards",
                newName: "IX_wards_administrative_unit_id");

            migrationBuilder.RenameIndex(
                name: "idx_wards_district",
                table: "wards",
                newName: "IX_wards_district_code");

            migrationBuilder.RenameIndex(
                name: "idx_provinces_unit",
                table: "provinces",
                newName: "IX_provinces_administrative_unit_id");

            migrationBuilder.RenameIndex(
                name: "idx_provinces_region",
                table: "provinces",
                newName: "IX_provinces_administrative_region_id");

            migrationBuilder.RenameIndex(
                name: "idx_districts_unit",
                table: "districts",
                newName: "IX_districts_administrative_unit_id");

            migrationBuilder.RenameIndex(
                name: "idx_districts_province",
                table: "districts",
                newName: "IX_districts_province_code");

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
    }
}
