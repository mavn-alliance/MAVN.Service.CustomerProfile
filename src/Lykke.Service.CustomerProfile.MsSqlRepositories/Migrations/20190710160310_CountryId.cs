using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class CountryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "country",
                schema: "customer_profile",
                table: "customer_profile_archive",
                newName: "short_phone_number");

            migrationBuilder.RenameColumn(
                name: "country",
                schema: "customer_profile",
                table: "customer_profile",
                newName: "short_phone_number");

            migrationBuilder.AddColumn<int>(
                name: "country_of_residence_id",
                schema: "customer_profile",
                table: "customer_profile_archive",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "country_phone_code_id",
                schema: "customer_profile",
                table: "customer_profile_archive",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "country_of_residence_id",
                schema: "customer_profile",
                table: "customer_profile",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "country_phone_code_id",
                schema: "customer_profile",
                table: "customer_profile",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "country_of_residence_id",
                schema: "customer_profile",
                table: "customer_profile_archive");

            migrationBuilder.DropColumn(
                name: "country_phone_code_id",
                schema: "customer_profile",
                table: "customer_profile_archive");

            migrationBuilder.DropColumn(
                name: "country_of_residence_id",
                schema: "customer_profile",
                table: "customer_profile");

            migrationBuilder.DropColumn(
                name: "country_phone_code_id",
                schema: "customer_profile",
                table: "customer_profile");

            migrationBuilder.RenameColumn(
                name: "short_phone_number",
                schema: "customer_profile",
                table: "customer_profile_archive",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "short_phone_number",
                schema: "customer_profile",
                table: "customer_profile",
                newName: "country");
        }
    }
}
