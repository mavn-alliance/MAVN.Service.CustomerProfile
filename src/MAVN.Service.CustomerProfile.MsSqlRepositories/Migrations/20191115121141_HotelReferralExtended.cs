using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class HotelReferralExtended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "customer_profile",
                table: "referral_hotel_profiles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                schema: "customer_profile",
                table: "referral_hotel_profiles",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                schema: "customer_profile",
                table: "referral_hotel_profiles");

            migrationBuilder.DropColumn(
                name: "phone_number",
                schema: "customer_profile",
                table: "referral_hotel_profiles");
        }
    }
}
