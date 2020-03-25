using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class AddCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "country",
                schema: "customer_profile",
                table: "customer_profile_archive",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "country",
                schema: "customer_profile",
                table: "customer_profile",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "country",
                schema: "customer_profile",
                table: "customer_profile_archive");

            migrationBuilder.DropColumn(
                name: "country",
                schema: "customer_profile",
                table: "customer_profile");
        }
    }
}
