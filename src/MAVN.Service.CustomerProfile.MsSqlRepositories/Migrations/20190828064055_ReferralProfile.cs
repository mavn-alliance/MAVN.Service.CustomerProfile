using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class ReferralProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "referral_hotel_profiles",
                schema: "customer_profile",
                columns: table => new
                {
                    referral_hotel_id = table.Column<Guid>(nullable: false),
                    email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_referral_hotel_profiles", x => x.referral_hotel_id);
                });

            migrationBuilder.CreateTable(
                name: "referral_hotel_profiles_archive",
                schema: "customer_profile",
                columns: table => new
                {
                    referral_hotel_id = table.Column<Guid>(nullable: false),
                    email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_referral_hotel_profiles_archive", x => x.referral_hotel_id);
                });

            migrationBuilder.CreateTable(
                name: "referral_lead_profiles",
                schema: "customer_profile",
                columns: table => new
                {
                    referral_lead_id = table.Column<Guid>(nullable: false),
                    first_name = table.Column<string>(nullable: false),
                    last_name = table.Column<string>(nullable: false),
                    phone_number = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_referral_lead_profiles", x => x.referral_lead_id);
                });

            migrationBuilder.CreateTable(
                name: "referral_lead_profiles_archive",
                schema: "customer_profile",
                columns: table => new
                {
                    referral_lead_id = table.Column<Guid>(nullable: false),
                    first_name = table.Column<string>(nullable: false),
                    last_name = table.Column<string>(nullable: false),
                    phone_number = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_referral_lead_profiles_archive", x => x.referral_lead_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_referral_hotel_profiles_email",
                schema: "customer_profile",
                table: "referral_hotel_profiles",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_referral_lead_profiles_email",
                schema: "customer_profile",
                table: "referral_lead_profiles",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_referral_lead_profiles_phone_number",
                schema: "customer_profile",
                table: "referral_lead_profiles",
                column: "phone_number");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "referral_hotel_profiles",
                schema: "customer_profile");

            migrationBuilder.DropTable(
                name: "referral_hotel_profiles_archive",
                schema: "customer_profile");

            migrationBuilder.DropTable(
                name: "referral_lead_profiles",
                schema: "customer_profile");

            migrationBuilder.DropTable(
                name: "referral_lead_profiles_archive",
                schema: "customer_profile");
        }
    }
}
