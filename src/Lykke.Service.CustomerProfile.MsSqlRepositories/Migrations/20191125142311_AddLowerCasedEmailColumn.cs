using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class AddLowerCasedEmailColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "lower_cased_email",
                schema: "customer_profile",
                table: "customer_profile",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(
                "UPDATE c SET c.lower_cased_email = c.email FROM [customer_profile].[customer_profile] c");

            migrationBuilder.CreateIndex(
                name: "IX_customer_profile_lower_cased_email",
                schema: "customer_profile",
                table: "customer_profile",
                column: "lower_cased_email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_customer_profile_lower_cased_email",
                schema: "customer_profile",
                table: "customer_profile");

            migrationBuilder.DropColumn(
                name: "lower_cased_email",
                schema: "customer_profile",
                table: "customer_profile");
        }
    }
}
