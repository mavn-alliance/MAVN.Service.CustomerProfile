using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class ChangeEnumTypeFromIntToStringInDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_customer_profile_Status",
                schema: "customer_profile",
                table: "customer_profile");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "customer_profile",
                table: "customer_profile",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.Sql("UPDATE c SET c.status = 'Active' FROM [customer_profile].[customer_profile] c");

            migrationBuilder.CreateIndex(
                name: "IX_customer_profile_Status",
                schema: "customer_profile",
                table: "customer_profile",
                column: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                schema: "customer_profile",
                table: "customer_profile",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
