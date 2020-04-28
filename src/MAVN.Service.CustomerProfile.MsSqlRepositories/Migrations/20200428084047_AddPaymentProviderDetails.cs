using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class AddPaymentProviderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "payment_provider_details",
                schema: "customer_profile",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    partner_id = table.Column<Guid>(nullable: false),
                    payment_integration_provider = table.Column<string>(nullable: false),
                    payment_integration_properties = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_provider_details", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payment_provider_details",
                schema: "customer_profile");
        }
    }
}
