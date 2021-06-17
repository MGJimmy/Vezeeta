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
                keyValue: "74064ffb-bc34-4f67-b480-a139eb375c00");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "602fda11-1f2d-417b-b263-a668d47c88ba", 0, "ebb03d65-3d90-4150-9aa1-14c717f1361c", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "5d2291cd-41ef-4509-bc8e-d0a6985dcf8d", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "602fda11-1f2d-417b-b263-a668d47c88ba");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "74064ffb-bc34-4f67-b480-a139eb375c00", 0, "8c27c7f4-8bb7-47af-ba29-96389e8ea3fe", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "f6c5c882-e443-4784-b966-0f72781247b9", false, "admin" });
        }
    }
}
