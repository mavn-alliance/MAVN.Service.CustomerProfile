using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class MakeCountryOfNationalityIdOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "country_of_nationality_id",
                schema: "customer_profile",
                table: "customer_profile_archive",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "country_of_nationality_id",
                schema: "customer_profile",
                table: "customer_profile",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "country_of_nationality_id",
                schema: "customer_profile",
                table: "customer_profile_archive",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "country_of_nationality_id",
                schema: "customer_profile",
                table: "customer_profile",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
