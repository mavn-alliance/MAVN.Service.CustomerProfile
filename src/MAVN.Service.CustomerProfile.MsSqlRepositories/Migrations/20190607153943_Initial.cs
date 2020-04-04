using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "customer_profile");

            migrationBuilder.CreateTable(
                name: "customer_profile",
                schema: "customer_profile",
                columns: table => new
                {
                    customer_id = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    registered_at = table.Column<DateTime>(nullable: false),
                    email_verified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_profile", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "customer_profile_archive",
                schema: "customer_profile",
                columns: table => new
                {
                    customer_id = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    email_verified = table.Column<bool>(nullable: false),
                    registered_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_profile_archive", x => x.customer_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customer_profile_customer_id",
                schema: "customer_profile",
                table: "customer_profile",
                column: "customer_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customer_profile_email",
                schema: "customer_profile",
                table: "customer_profile",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_customer_profile_archive_customer_id",
                schema: "customer_profile",
                table: "customer_profile_archive",
                column: "customer_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer_profile",
                schema: "customer_profile");

            migrationBuilder.DropTable(
                name: "customer_profile_archive",
                schema: "customer_profile");
        }
    }
}
