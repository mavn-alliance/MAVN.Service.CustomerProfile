using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class AddIndicesForPaymentProviderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "payment_integration_provider",
                schema: "customer_profile",
                table: "payment_provider_details",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_payment_provider_details_partner_id",
                schema: "customer_profile",
                table: "payment_provider_details",
                column: "partner_id");

            migrationBuilder.CreateIndex(
                name: "IX_payment_provider_details_partner_id_payment_integration_provider",
                schema: "customer_profile",
                table: "payment_provider_details",
                columns: new[] { "partner_id", "payment_integration_provider" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_payment_provider_details_partner_id",
                schema: "customer_profile",
                table: "payment_provider_details");

            migrationBuilder.DropIndex(
                name: "IX_payment_provider_details_partner_id_payment_integration_provider",
                schema: "customer_profile",
                table: "payment_provider_details");

            migrationBuilder.AlterColumn<string>(
                name: "payment_integration_provider",
                schema: "customer_profile",
                table: "payment_provider_details",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
