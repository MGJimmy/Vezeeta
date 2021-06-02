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
                keyValue: "7d2a4d84-641d-42e7-a829-310d3e5933e4");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "bcfa4dc3-9c33-4970-8231-776f897e2919", 0, "d27294ed-5a2f-4899-b0f9-eada2bf74272", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "43ec44f4-af6c-4c2c-9798-5fdd16c806ef", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcfa4dc3-9c33-4970-8231-776f897e2919");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7d2a4d84-641d-42e7-a829-310d3e5933e4", 0, "aade3dc5-bf4e-4983-946a-61cdd372d16e", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "2b04c682-0caa-4829-9799-a63cf3577658", false, "admin" });
        }
    }
}
