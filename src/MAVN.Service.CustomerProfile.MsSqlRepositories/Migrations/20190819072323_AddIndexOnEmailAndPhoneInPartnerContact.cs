using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class AddIndexOnEmailAndPhoneInPartnerContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "phone_number",
                schema: "customer_profile",
                table: "partner_contact",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                schema: "customer_profile",
                table: "partner_contact",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_partner_contact_email",
                schema: "customer_profile",
                table: "partner_contact",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_partner_contact_phone_number",
                schema: "customer_profile",
                table: "partner_contact",
                column: "phone_number");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_partner_contact_email",
                schema: "customer_profile",
                table: "partner_contact");

            migrationBuilder.DropIndex(
                name: "IX_partner_contact_phone_number",
                schema: "customer_profile",
                table: "partner_contact");

            migrationBuilder.AlterColumn<string>(
                name: "phone_number",
                schema: "customer_profile",
                table: "partner_contact",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                schema: "customer_profile",
                table: "partner_contact",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
