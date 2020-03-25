using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class AddLoginProviders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "login_providers",
                schema: "customer_profile",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    customer_id = table.Column<string>(nullable: false),
                    login_provider = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_login_providers", x => x.id);
                    table.ForeignKey(
                        name: "FK_login_providers_customer_profile_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "customer_profile",
                        principalTable: "customer_profile",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_login_providers_customer_id",
                schema: "customer_profile",
                table: "login_providers",
                column: "customer_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "login_providers",
                schema: "customer_profile");
        }
    }
}
