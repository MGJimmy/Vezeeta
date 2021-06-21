using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d1a3af71-41b8-47d9-a84a-aa84b67eb978");

            migrationBuilder.AddColumn<double>(
                name: "CountOfRating",
                table: "Doctor",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SumOfRating",
                table: "Doctor",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1d448bc1-7c4f-49dc-a20e-fd05973f3b66", 0, "7eec846c-6a4a-4957-b020-282cd5702606", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "336ccf5f-da67-4a18-92c2-8c92257a1261", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d448bc1-7c4f-49dc-a20e-fd05973f3b66");

            migrationBuilder.DropColumn(
                name: "CountOfRating",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "SumOfRating",
                table: "Doctor");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d1a3af71-41b8-47d9-a84a-aa84b67eb978", 0, "8bc0cdb0-6098-44cc-ab41-80db64825e9d", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "83b1fba7-0f6c-4909-804e-1a50ca6ecaaf", false, "admin" });
        }
    }
}
