using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class CountryPhoneCodeRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"update customer_profile.customer_profile_archive set country_phone_code_id = 1 where country_phone_code_id is null");
            
            migrationBuilder.AlterColumn<int>(
                name: "country_phone_code_id",
                schema: "customer_profile",
                table: "customer_profile_archive",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.Sql(@"update customer_profile.customer_profile set country_phone_code_id = 1 where country_phone_code_id is null");
            
            migrationBuilder.AlterColumn<int>(
                name: "country_phone_code_id",
                schema: "customer_profile",
                table: "customer_profile",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "country_phone_code_id",
                schema: "customer_profile",
                table: "customer_profile_archive",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "country_phone_code_id",
                schema: "customer_profile",
                table: "customer_profile",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
