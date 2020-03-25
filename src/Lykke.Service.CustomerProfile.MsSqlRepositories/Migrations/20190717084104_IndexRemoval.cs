using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class IndexRemoval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_customer_profile_archive_customer_id",
                schema: "customer_profile",
                table: "customer_profile_archive");

            migrationBuilder.DropIndex(
                name: "IX_customer_profile_customer_id",
                schema: "customer_profile",
                table: "customer_profile");

            migrationBuilder.DropIndex(
                name: "IX_customer_profile_email",
                schema: "customer_profile",
                table: "customer_profile");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                schema: "customer_profile",
                table: "customer_profile_archive",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_customer_profile_archive_email",
                schema: "customer_profile",
                table: "customer_profile_archive",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_customer_profile_email",
                schema: "customer_profile",
                table: "customer_profile",
                column: "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_customer_profile_archive_email",
                schema: "customer_profile",
                table: "customer_profile_archive");

            migrationBuilder.DropIndex(
                name: "IX_customer_profile_email",
                schema: "customer_profile",
                table: "customer_profile");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                schema: "customer_profile",
                table: "customer_profile_archive",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_customer_profile_archive_customer_id",
                schema: "customer_profile",
                table: "customer_profile_archive",
                column: "customer_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customer_profile_customer_id",
                schema: "customer_profile",
                table: "customer_profile",
                column: "customer_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customer_profile_email",
                schema: "customer_profile",
                table: "customer_profile",
                column: "email");
        }
    }
}
