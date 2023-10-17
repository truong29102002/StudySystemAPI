using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudySystem.Data.EF.Migrations
{
    public partial class v11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "UserDetails");

            migrationBuilder.CreateTable(
                name: "AddressUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Descriptions = table.Column<string>(type: "text", nullable: false),
                    ward_code = table.Column<string>(type: "varchar(20)", nullable: false),
                    districts_code = table.Column<string>(type: "varchar(20)", nullable: false),
                    province_code = table.Column<string>(type: "varchar(20)", nullable: false),
                    UserID = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    CreateUser = table.Column<string>(type: "text", nullable: false),
                    UpdateUser = table.Column<string>(type: "text", nullable: false),
                    CreateDateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateDateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressUsers_UserDetails_UserID",
                        column: x => x.UserID,
                        principalTable: "UserDetails",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDetail_UserID",
                table: "UserDetails",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_AddressUser_UserID",
                table: "AddressUsers",
                column: "UserID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressUsers");

            migrationBuilder.DropIndex(
                name: "IX_UserDetail_UserID",
                table: "UserDetails");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "UserDetails",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
