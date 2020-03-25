using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class RenameWasPhoneVerifiedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "was_phone_verified",
                schema: "customer_profile",
                table: "customer_profile_archive",
                newName: "was_phone_ever_verified");

            migrationBuilder.RenameColumn(
                name: "was_phone_verified",
                schema: "customer_profile",
                table: "customer_profile",
                newName: "was_phone_ever_verified");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "was_phone_ever_verified",
                schema: "customer_profile",
                table: "customer_profile_archive",
                newName: "was_phone_verified");

            migrationBuilder.RenameColumn(
                name: "was_phone_ever_verified",
                schema: "customer_profile",
                table: "customer_profile",
                newName: "was_phone_verified");
        }
    }
}
