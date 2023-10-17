using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StudySystem.Data.EF.Migrations
{
    public partial class v7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "wards",
                columns: table => new
                {
                    code = table.Column<string>(type: "varchar(20)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    name_en = table.Column<string>(type: "varchar(255)", nullable: false),
                    full_name = table.Column<string>(type: "varchar(255)", nullable: false),
                    full_name_en = table.Column<string>(type: "varchar(255)", nullable: false),
                    code_name = table.Column<string>(type: "varchar(255)", nullable: false),
                    district_code = table.Column<int>(type: "integer", nullable: false),
                    administrative_unit_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wards", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "districts",
                columns: table => new
                {
                    code = table.Column<string>(type: "varchar(20)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    name_en = table.Column<string>(type: "varchar(255)", nullable: false),
                    full_name = table.Column<string>(type: "varchar(255)", nullable: false),
                    full_name_en = table.Column<string>(type: "varchar(255)", nullable: false),
                    code_name = table.Column<string>(type: "varchar(255)", nullable: false),
                    province_code = table.Column<int>(type: "integer", nullable: false),
                    administrative_unit_id = table.Column<int>(type: "integer", nullable: false),
                    district_code = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_districts", x => x.code);
                    table.ForeignKey(
                        name: "FK_districts_wards_district_code",
                        column: x => x.district_code,
                        principalTable: "wards",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "provinces",
                columns: table => new
                {
                    code = table.Column<string>(type: "varchar(20)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    name_en = table.Column<string>(type: "varchar(255)", nullable: false),
                    full_name = table.Column<string>(type: "varchar(255)", nullable: false),
                    full_name_en = table.Column<string>(type: "varchar(255)", nullable: false),
                    code_name = table.Column<string>(type: "varchar(255)", nullable: false),
                    administrative_unit_id = table.Column<int>(type: "integer", nullable: false),
                    administrative_region_id = table.Column<int>(type: "integer", nullable: false),
                    province_code = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provinces", x => x.code);
                    table.ForeignKey(
                        name: "FK_provinces_districts_province_code",
                        column: x => x.province_code,
                        principalTable: "districts",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "administrative_regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    name_en = table.Column<string>(type: "varchar(255)", nullable: false),
                    code_name = table.Column<string>(type: "varchar(255)", nullable: false),
                    code_name_en = table.Column<string>(type: "varchar(255)", nullable: false),
                    administrative_region_id = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administrative_regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_administrative_regions_provinces_administrative_region_id",
                        column: x => x.administrative_region_id,
                        principalTable: "provinces",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "administrative_units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    full_name = table.Column<string>(type: "varchar(255)", nullable: false),
                    full_name_en = table.Column<string>(type: "varchar(255)", nullable: false),
                    short_name = table.Column<string>(type: "varchar(255)", nullable: false),
                    short_name_en = table.Column<string>(type: "varchar(255)", nullable: false),
                    code_name = table.Column<string>(type: "varchar(255)", nullable: false),
                    code_name_en = table.Column<string>(type: "varchar(255)", nullable: false),
                    administrative_unit_id = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administrative_units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_administrative_units_districts_administrative_unit_id",
                        column: x => x.administrative_unit_id,
                        principalTable: "districts",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_administrative_units_provinces_administrative_unit_id",
                        column: x => x.administrative_unit_id,
                        principalTable: "provinces",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_administrative_units_wards_administrative_unit_id",
                        column: x => x.administrative_unit_id,
                        principalTable: "wards",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_administrative_regions_administrative_region_id",
                table: "administrative_regions",
                column: "administrative_region_id");

            migrationBuilder.CreateIndex(
                name: "IX_administrative_units_administrative_unit_id",
                table: "administrative_units",
                column: "administrative_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_districts_district_code",
                table: "districts",
                column: "district_code");

            migrationBuilder.CreateIndex(
                name: "IX_provinces_province_code",
                table: "provinces",
                column: "province_code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "administrative_regions");

            migrationBuilder.DropTable(
                name: "administrative_units");

            migrationBuilder.DropTable(
                name: "provinces");

            migrationBuilder.DropTable(
                name: "districts");

            migrationBuilder.DropTable(
                name: "wards");
        }
    }
}
