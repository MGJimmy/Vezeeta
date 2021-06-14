using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "afccdacf-53ff-4370-ae14-a239f4ceb464");

            migrationBuilder.CreateTable(
                name: "ReserveOffer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dayShiftId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    doctorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReserveOffer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReserveOffer_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReserveOffer_DayShift_dayShiftId",
                        column: x => x.dayShiftId,
                        principalTable: "DayShift",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReserveOffer_Doctor_doctorId",
                        column: x => x.doctorId,
                        principalTable: "Doctor",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReserveOffer_MakeOffers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "MakeOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b4efe154-78e6-47d3-9301-417214588196", 0, "21bc0356-f0cc-470e-856f-12f3bdef6a95", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "31c6d41b-4191-4ab2-a806-80d327afbf57", false, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_ReserveOffer_dayShiftId",
                table: "ReserveOffer",
                column: "dayShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_ReserveOffer_doctorId",
                table: "ReserveOffer",
                column: "doctorId");

            migrationBuilder.CreateIndex(
                name: "IX_ReserveOffer_OfferId",
                table: "ReserveOffer",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_ReserveOffer_userId",
                table: "ReserveOffer",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReserveOffer");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b4efe154-78e6-47d3-9301-417214588196");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "afccdacf-53ff-4370-ae14-a239f4ceb464", 0, "b4ab4288-1a05-4d7f-ae93-6c8aea883186", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "6278550d-5218-46be-b7e1-394bf71fe8fe", false, "admin" });
        }
    }
}
