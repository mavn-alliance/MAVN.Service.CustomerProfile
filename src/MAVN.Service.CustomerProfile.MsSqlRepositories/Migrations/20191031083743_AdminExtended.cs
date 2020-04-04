using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class AdminExtended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "company",
                schema: "customer_profile",
                table: "admin_profiles_archive",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "department",
                schema: "customer_profile",
                table: "admin_profiles_archive",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "job_title",
                schema: "customer_profile",
                table: "admin_profiles_archive",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                schema: "customer_profile",
                table: "admin_profiles_archive",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "company",
                schema: "customer_profile",
                table: "admin_profiles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "department",
                schema: "customer_profile",
                table: "admin_profiles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "job_title",
                schema: "customer_profile",
                table: "admin_profiles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                schema: "customer_profile",
                table: "admin_profiles",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "company",
                schema: "customer_profile",
                table: "admin_profiles_archive");

            migrationBuilder.DropColumn(
                name: "department",
                schema: "customer_profile",
                table: "admin_profiles_archive");

            migrationBuilder.DropColumn(
                name: "job_title",
                schema: "customer_profile",
                table: "admin_profiles_archive");

            migrationBuilder.DropColumn(
                name: "phone_number",
                schema: "customer_profile",
                table: "admin_profiles_archive");

            migrationBuilder.DropColumn(
                name: "company",
                schema: "customer_profile",
                table: "admin_profiles");

            migrationBuilder.DropColumn(
                name: "department",
                schema: "customer_profile",
                table: "admin_profiles");

            migrationBuilder.DropColumn(
                name: "job_title",
                schema: "customer_profile",
                table: "admin_profiles");

            migrationBuilder.DropColumn(
                name: "phone_number",
                schema: "customer_profile",
                table: "admin_profiles");
        }
    }
}
