using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class Tier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "tier_id",
                schema: "customer_profile",
                table: "customer_profile_archive",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tier_id",
                schema: "customer_profile",
                table: "customer_profile",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tier_id",
                schema: "customer_profile",
                table: "customer_profile_archive");

            migrationBuilder.DropColumn(
                name: "tier_id",
                schema: "customer_profile",
                table: "customer_profile");
        }
    }
}
