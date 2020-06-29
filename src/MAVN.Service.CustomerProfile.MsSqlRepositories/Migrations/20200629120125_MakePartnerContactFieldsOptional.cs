using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class MakePartnerContactFieldsOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "email",
                schema: "customer_profile",
                table: "partner_contact_archive",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "email",
                schema: "customer_profile",
                table: "partner_contact",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "email",
                schema: "customer_profile",
                table: "partner_contact_archive",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                schema: "customer_profile",
                table: "partner_contact",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
