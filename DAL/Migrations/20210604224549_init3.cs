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
                keyValue: "8c456b31-f4af-424d-b31f-af3db48d07b8");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7029154f-1cad-42e4-9d55-7c29855d5baa", 0, "b2779560-6e25-438d-aa44-0e8805427c31", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "5e4479a6-b9fd-4e76-9fb2-3ca9bfabd981", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7029154f-1cad-42e4-9d55-7c29855d5baa");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8c456b31-f4af-424d-b31f-af3db48d07b8", 0, "718b8182-5fb0-4926-800a-b8c159f94569", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "a55c63b3-0238-4a50-ae97-c38632530cf5", false, "admin" });
        }
    }
}
