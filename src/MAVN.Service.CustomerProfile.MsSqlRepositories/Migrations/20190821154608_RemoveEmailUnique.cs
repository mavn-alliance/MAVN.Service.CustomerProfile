using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class RemoveEmailUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_partner_contact_email",
                schema: "customer_profile",
                table: "partner_contact");

            migrationBuilder.CreateIndex(
                name: "IX_partner_contact_email",
                schema: "customer_profile",
                table: "partner_contact",
                column: "email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_partner_contact_email",
                schema: "customer_profile",
                table: "partner_contact");

            migrationBuilder.CreateIndex(
                name: "IX_partner_contact_email",
                schema: "customer_profile",
                table: "partner_contact",
                column: "email",
                unique: true);
        }
    }
}
