using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class AddPhoneVerificationProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "phone_verified",
                schema: "customer_profile",
                table: "customer_profile_archive",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "was_phone_verified",
                schema: "customer_profile",
                table: "customer_profile_archive",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "phone_verified",
                schema: "customer_profile",
                table: "customer_profile",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "was_phone_verified",
                schema: "customer_profile",
                table: "customer_profile",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "phone_verified",
                schema: "customer_profile",
                table: "customer_profile_archive");

            migrationBuilder.DropColumn(
                name: "was_phone_verified",
                schema: "customer_profile",
                table: "customer_profile_archive");

            migrationBuilder.DropColumn(
                name: "phone_verified",
                schema: "customer_profile",
                table: "customer_profile");

            migrationBuilder.DropColumn(
                name: "was_phone_verified",
                schema: "customer_profile",
                table: "customer_profile");
        }
    }
}
