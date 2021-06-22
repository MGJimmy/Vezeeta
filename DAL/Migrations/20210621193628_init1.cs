using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f5d777a7-3134-4a45-87ec-8e493b8348f4");

            migrationBuilder.CreateTable(
                name: "OfferRatings",
                columns: table => new
                {
                    ReserveOfferId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MakeOfferId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferRatings", x => x.ReserveOfferId);
                    table.ForeignKey(
                        name: "FK_OfferRatings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferRatings_MakeOffers_MakeOfferId",
                        column: x => x.MakeOfferId,
                        principalTable: "MakeOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferRatings_ReserveOffer_ReserveOfferId",
                        column: x => x.ReserveOfferId,
                        principalTable: "ReserveOffer",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4fb1f113-618f-44ae-af0f-78ea58859d25", 0, "fd700424-4e1a-4d90-8d66-2fda637ddaa7", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "b3bacf8a-ebd5-4b74-aa89-14403e26bc3e", false, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_OfferRatings_MakeOfferId",
                table: "OfferRatings",
                column: "MakeOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferRatings_UserId",
                table: "OfferRatings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferRatings");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4fb1f113-618f-44ae-af0f-78ea58859d25");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f5d777a7-3134-4a45-87ec-8e493b8348f4", 0, "ba2644d7-88b2-4e54-a989-6a90585f699f", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "aa092e07-66e9-405b-98b1-42d75e43f44e", false, "admin" });
        }
    }
}
