using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MAVN.Service.CustomerProfile.MsSqlRepositories.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "customer_profile");

            migrationBuilder.CreateTable(
                name: "admin_profiles",
                schema: "customer_profile",
                columns: table => new
                {
                    admin_id = table.Column<Guid>(nullable: false),
                    first_name = table.Column<string>(nullable: false),
                    last_name = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    email_verified = table.Column<bool>(nullable: false),
                    was_email_ever_verified = table.Column<bool>(nullable: false),
                    phone_number = table.Column<string>(nullable: false),
                    company = table.Column<string>(nullable: false),
                    department = table.Column<string>(nullable: false),
                    job_title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin_profiles", x => x.admin_id);
                });

            migrationBuilder.CreateTable(
                name: "admin_profiles_archive",
                schema: "customer_profile",
                columns: table => new
                {
                    admin_id = table.Column<Guid>(nullable: false),
                    first_name = table.Column<string>(nullable: false),
                    last_name = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    email_verified = table.Column<bool>(nullable: false),
                    was_email_ever_verified = table.Column<bool>(nullable: false),
                    phone_number = table.Column<string>(nullable: false),
                    company = table.Column<string>(nullable: false),
                    department = table.Column<string>(nullable: false),
                    job_title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin_profiles_archive", x => x.admin_id);
                });

            migrationBuilder.CreateTable(
                name: "customer_profile",
                schema: "customer_profile",
                columns: table => new
                {
                    customer_id = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    lower_cased_email = table.Column<string>(nullable: false),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    short_phone_number = table.Column<string>(nullable: true),
                    country_phone_code_id = table.Column<int>(nullable: true),
                    country_of_residence_id = table.Column<int>(nullable: true),
                    country_of_nationality_id = table.Column<int>(nullable: true),
                    tier_id = table.Column<string>(nullable: true),
                    registered_at = table.Column<DateTime>(nullable: false),
                    email_verified = table.Column<bool>(nullable: false),
                    was_email_ever_verified = table.Column<bool>(nullable: false),
                    phone_verified = table.Column<bool>(nullable: false),
                    was_phone_ever_verified = table.Column<bool>(nullable: false),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_profile", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "customer_profile_archive",
                schema: "customer_profile",
                columns: table => new
                {
                    customer_id = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    short_phone_number = table.Column<string>(nullable: true),
                    country_phone_code_id = table.Column<int>(nullable: true),
                    tier_id = table.Column<string>(nullable: true),
                    country_of_residence_id = table.Column<int>(nullable: true),
                    country_of_nationality_id = table.Column<int>(nullable: true),
                    email_verified = table.Column<bool>(nullable: false),
                    phone_verified = table.Column<bool>(nullable: false),
                    was_phone_ever_verified = table.Column<bool>(nullable: false),
                    registered_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_profile_archive", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "partner_contact",
                schema: "customer_profile",
                columns: table => new
                {
                    location_id = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: true),
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
                    email = table.Column<string>(nullable: true),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partner_contact_archive", x => x.location_id);
                });

            migrationBuilder.CreateTable(
                name: "payment_provider_details",
                schema: "customer_profile",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    partner_id = table.Column<Guid>(nullable: false),
                    payment_integration_provider = table.Column<string>(nullable: false),
                    payment_integration_properties = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_provider_details", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "referral_friend_profiles",
                schema: "customer_profile",
                columns: table => new
                {
                    referral_friend_id = table.Column<Guid>(nullable: false),
                    referrer_id = table.Column<Guid>(nullable: false),
                    full_name = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_referral_friend_profiles", x => x.referral_friend_id);
                });

            migrationBuilder.CreateTable(
                name: "referral_friend_profiles_archive",
                schema: "customer_profile",
                columns: table => new
                {
                    referral_friend_id = table.Column<Guid>(nullable: false),
                    referrer_id = table.Column<Guid>(nullable: false),
                    full_name = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_referral_friend_profiles_archive", x => x.referral_friend_id);
                });

            migrationBuilder.CreateTable(
                name: "referral_hotel_profiles",
                schema: "customer_profile",
                columns: table => new
                {
                    referral_hotel_id = table.Column<Guid>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    phone_number = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_referral_hotel_profiles", x => x.referral_hotel_id);
                });

            migrationBuilder.CreateTable(
                name: "referral_hotel_profiles_archive",
                schema: "customer_profile",
                columns: table => new
                {
                    referral_hotel_id = table.Column<Guid>(nullable: false),
                    email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_referral_hotel_profiles_archive", x => x.referral_hotel_id);
                });

            migrationBuilder.CreateTable(
                name: "referral_lead_profiles",
                schema: "customer_profile",
                columns: table => new
                {
                    referral_lead_id = table.Column<Guid>(nullable: false),
                    first_name = table.Column<string>(nullable: false),
                    last_name = table.Column<string>(nullable: false),
                    phone_number = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_referral_lead_profiles", x => x.referral_lead_id);
                });

            migrationBuilder.CreateTable(
                name: "referral_lead_profiles_archive",
                schema: "customer_profile",
                columns: table => new
                {
                    referral_lead_id = table.Column<Guid>(nullable: false),
                    first_name = table.Column<string>(nullable: false),
                    last_name = table.Column<string>(nullable: false),
                    phone_number = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_referral_lead_profiles_archive", x => x.referral_lead_id);
                });

            migrationBuilder.CreateTable(
                name: "login_providers",
                schema: "customer_profile",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                name: "IX_admin_profiles_email",
                schema: "customer_profile",
                table: "admin_profiles",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_customer_profile_email",
                schema: "customer_profile",
                table: "customer_profile",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customer_profile_lower_cased_email",
                schema: "customer_profile",
                table: "customer_profile",
                column: "lower_cased_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customer_profile_Status",
                schema: "customer_profile",
                table: "customer_profile",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_customer_profile_archive_email",
                schema: "customer_profile",
                table: "customer_profile_archive",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_login_providers_customer_id",
                schema: "customer_profile",
                table: "login_providers",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_partner_contact_email",
                schema: "customer_profile",
                table: "partner_contact",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_partner_contact_phone_number",
                schema: "customer_profile",
                table: "partner_contact",
                column: "phone_number");

            migrationBuilder.CreateIndex(
                name: "IX_payment_provider_details_partner_id",
                schema: "customer_profile",
                table: "payment_provider_details",
                column: "partner_id");

            migrationBuilder.CreateIndex(
                name: "IX_payment_provider_details_partner_id_payment_integration_pro~",
                schema: "customer_profile",
                table: "payment_provider_details",
                columns: new[] { "partner_id", "payment_integration_provider" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_referral_friend_profiles_email",
                schema: "customer_profile",
                table: "referral_friend_profiles",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_referral_friend_profiles_referrer_id",
                schema: "customer_profile",
                table: "referral_friend_profiles",
                column: "referrer_id");

            migrationBuilder.CreateIndex(
                name: "IX_referral_hotel_profiles_email",
                schema: "customer_profile",
                table: "referral_hotel_profiles",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_referral_lead_profiles_email",
                schema: "customer_profile",
                table: "referral_lead_profiles",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_referral_lead_profiles_phone_number",
                schema: "customer_profile",
                table: "referral_lead_profiles",
                column: "phone_number");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin_profiles",
                schema: "customer_profile");

            migrationBuilder.DropTable(
                name: "admin_profiles_archive",
                schema: "customer_profile");

            migrationBuilder.DropTable(
                name: "customer_profile_archive",
                schema: "customer_profile");

            migrationBuilder.DropTable(
                name: "login_providers",
                schema: "customer_profile");

            migrationBuilder.DropTable(
                name: "partner_contact",
                schema: "customer_profile");

            migrationBuilder.DropTable(
                name: "partner_contact_archive",
                schema: "customer_profile");

            migrationBuilder.DropTable(
                name: "payment_provider_details",
                schema: "customer_profile");

            migrationBuilder.DropTable(
                name: "referral_friend_profiles",
                schema: "customer_profile");

            migrationBuilder.DropTable(
                name: "referral_friend_profiles_archive",
                schema: "customer_profile");

            migrationBuilder.DropTable(
                name: "referral_hotel_profiles",
                schema: "customer_profile");

            migrationBuilder.DropTable(
                name: "referral_hotel_profiles_archive",
                schema: "customer_profile");

            migrationBuilder.DropTable(
                name: "referral_lead_profiles",
                schema: "customer_profile");

            migrationBuilder.DropTable(
                name: "referral_lead_profiles_archive",
                schema: "customer_profile");

            migrationBuilder.DropTable(
                name: "customer_profile",
                schema: "customer_profile");
        }
    }
}
