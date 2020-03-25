using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class AddIndexToFriendReferral : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_referral_friend_profiles_referral_friend_id",
                schema: "customer_profile",
                table: "referral_friend_profiles");

            migrationBuilder.CreateIndex(
                name: "IX_referral_friend_profiles_referrer_id",
                schema: "customer_profile",
                table: "referral_friend_profiles",
                column: "referrer_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_referral_friend_profiles_referrer_id",
                schema: "customer_profile",
                table: "referral_friend_profiles");

            migrationBuilder.CreateIndex(
                name: "IX_referral_friend_profiles_referral_friend_id",
                schema: "customer_profile",
                table: "referral_friend_profiles",
                column: "referral_friend_id");
        }
    }
}
