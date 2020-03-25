using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class AddPartnerContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "partner_contact",
                schema: "customer_profile",
                columns: table => new
                {
                    location_id = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partner_contact", x => x.location_id);
                });

            migrationBuilder.CreateTable(
                name: "partner_contact_archive",
                schema: "customer_profile",
                columns: table => new
                {
                    location_id = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partner_contact_archive", x => x.location_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "partner_contact",
                schema: "customer_profile");

            migrationBuilder.DropTable(
                name: "partner_contact_archive",
                schema: "customer_profile");
        }
    }
}
