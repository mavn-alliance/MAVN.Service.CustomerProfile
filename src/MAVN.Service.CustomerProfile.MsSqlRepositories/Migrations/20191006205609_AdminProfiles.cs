using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class AdminProfiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admin_profiles",
                schema: "customer_profile",
                columns: table => new
                {
                    admin_id = table.Column<Guid>(nullable: false),
                    first_name = table.Column<string>(nullable: false),
                    last_name = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin_profiles", x => x.admin_id);
                });

            migrationBuilder.CreateTable(
                name: "admin_profiles_archive",
                schema: "customer_profile",
                columns: table => new
                {
                    admin_id = table.Column<Guid>(nullable: false),
                    first_name = table.Column<string>(nullable: false),
                    last_name = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin_profiles_archive", x => x.admin_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_admin_profiles_email",
                schema: "customer_profile",
                table: "admin_profiles",
                column: "email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin_profiles",
                schema: "customer_profile");

            migrationBuilder.DropTable(
                name: "admin_profiles_archive",
                schema: "customer_profile");
        }
    }
}
