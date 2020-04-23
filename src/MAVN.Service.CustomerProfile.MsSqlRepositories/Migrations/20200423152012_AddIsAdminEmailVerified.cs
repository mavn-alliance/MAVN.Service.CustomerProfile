using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class AddIsAdminEmailVerified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "email_verified",
                schema: "customer_profile",
                table: "admin_profiles_archive",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "was_email_ever_verified",
                schema: "customer_profile",
                table: "admin_profiles_archive",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "email_verified",
                schema: "customer_profile",
                table: "admin_profiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "was_email_ever_verified",
                schema: "customer_profile",
                table: "admin_profiles",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email_verified",
                schema: "customer_profile",
                table: "admin_profiles_archive");

            migrationBuilder.DropColumn(
                name: "was_email_ever_verified",
                schema: "customer_profile",
                table: "admin_profiles_archive");

            migrationBuilder.DropColumn(
                name: "email_verified",
                schema: "customer_profile",
                table: "admin_profiles");

            migrationBuilder.DropColumn(
                name: "was_email_ever_verified",
                schema: "customer_profile",
                table: "admin_profiles");
        }
    }
}
