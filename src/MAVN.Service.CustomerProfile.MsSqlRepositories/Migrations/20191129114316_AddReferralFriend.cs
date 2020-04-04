using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class AddReferralFriend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "referral_friend_profiles",
                schema: "customer_profile",
                columns: table => new
                {
                    referral_friend_id = table.Column<Guid>(nullable: false),
                    referrer_id = table.Column<Guid>(nullable: false),
                    first_name = table.Column<string>(nullable: false),
                    last_name = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_referral_friend_profiles", x => x.referral_friend_id);
                });

            migrationBuilder.CreateTable(
                name: "referral_friend_profiles_archive",
                schema: "customer_profile",
                columns: table => new
                {
                    referral_friend_id = table.Column<Guid>(nullable: false),
                    referrer_id = table.Column<Guid>(nullable: false),
                    first_name = table.Column<string>(nullable: false),
                    last_name = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_referral_friend_profiles_archive", x => x.referral_friend_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "referral_friend_profiles",
                schema: "customer_profile");

            migrationBuilder.DropTable(
                name: "referral_friend_profiles_archive",
                schema: "customer_profile");
        }
    }
}
