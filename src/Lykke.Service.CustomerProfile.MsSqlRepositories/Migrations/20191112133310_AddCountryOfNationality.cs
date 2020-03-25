using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class AddCountryOfNationality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "country_of_nationality_id",
                schema: "customer_profile",
                table: "customer_profile_archive",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "country_of_nationality_id",
                schema: "customer_profile",
                table: "customer_profile",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "country_of_nationality_id",
                schema: "customer_profile",
                table: "customer_profile_archive");

            migrationBuilder.DropColumn(
                name: "country_of_nationality_id",
                schema: "customer_profile",
                table: "customer_profile");
        }
    }
}
