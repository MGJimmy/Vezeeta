using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class jimmy2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9d9e3e47-d4f4-4a4a-bde6-d983950ca1ac");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Specialty",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "127f4f6e-70b3-442f-8ee2-4ccf8a1546b8", 0, "f0cba1f6-a59e-4c38-8a12-2e61c90dcaf6", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "20609cad-24a2-469c-ac05-458409ac8ca8", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "127f4f6e-70b3-442f-8ee2-4ccf8a1546b8");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Specialty");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9d9e3e47-d4f4-4a4a-bde6-d983950ca1ac", 0, "99ce124b-6ed0-4d96-a251-bca0b7172404", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "0bc258cc-5bb5-4d56-a68c-f5b66c097eba", false, "admin" });
        }
    }
}
