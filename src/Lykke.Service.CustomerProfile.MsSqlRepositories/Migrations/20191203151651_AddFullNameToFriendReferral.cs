using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class AddFullNameToFriendReferral : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "first_name",
                schema: "customer_profile",
                table: "referral_friend_profiles_archive");

            migrationBuilder.DropColumn(
                name: "first_name",
                schema: "customer_profile",
                table: "referral_friend_profiles");

            migrationBuilder.RenameColumn(
                name: "last_name",
                schema: "customer_profile",
                table: "referral_friend_profiles_archive",
                newName: "full_name");

            migrationBuilder.RenameColumn(
                name: "last_name",
                schema: "customer_profile",
                table: "referral_friend_profiles",
                newName: "full_name");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                schema: "customer_profile",
                table: "referral_friend_profiles",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_referral_friend_profiles_email",
                schema: "customer_profile",
                table: "referral_friend_profiles",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_referral_friend_profiles_referral_friend_id",
                schema: "customer_profile",
                table: "referral_friend_profiles",
                column: "referral_friend_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_referral_friend_profiles_email",
                schema: "customer_profile",
                table: "referral_friend_profiles");

            migrationBuilder.DropIndex(
                name: "IX_referral_friend_profiles_referral_friend_id",
                schema: "customer_profile",
                table: "referral_friend_profiles");

            migrationBuilder.RenameColumn(
                name: "full_name",
                schema: "customer_profile",
                table: "referral_friend_profiles_archive",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "full_name",
                schema: "customer_profile",
                table: "referral_friend_profiles",
                newName: "last_name");

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                schema: "customer_profile",
                table: "referral_friend_profiles_archive",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                schema: "customer_profile",
                table: "referral_friend_profiles",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                schema: "customer_profile",
                table: "referral_friend_profiles",
                nullable: false,
                defaultValue: "");
        }
    }
}
