using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "63334abb-994c-4d3e-a58e-b88f9c94e7b1");

            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                table: "MakeOffers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "afccdacf-53ff-4370-ae14-a239f4ceb464", 0, "b4ab4288-1a05-4d7f-ae93-6c8aea883186", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "6278550d-5218-46be-b7e1-394bf71fe8fe", false, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_MakeOffers_DoctorId",
                table: "MakeOffers",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_MakeOffers_Doctor_DoctorId",
                table: "MakeOffers",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MakeOffers_Doctor_DoctorId",
                table: "MakeOffers");

            migrationBuilder.DropIndex(
                name: "IX_MakeOffers_DoctorId",
                table: "MakeOffers");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "afccdacf-53ff-4370-ae14-a239f4ceb464");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "MakeOffers");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "63334abb-994c-4d3e-a58e-b88f9c94e7b1", 0, "97c3da54-7c8f-45d2-88ca-8a0bd05ee863", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "76c5a478-ecf5-4cae-bdea-167aaf02d5e5", false, "admin" });
        }
    }
}
