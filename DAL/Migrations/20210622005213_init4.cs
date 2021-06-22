using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "206c1816-bee7-4c00-8c5d-a8031b618ad2");

            migrationBuilder.AddColumn<bool>(
                name: "Rejected",
                table: "DoctorAttachment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9f6d573f-6a2c-4ca1-b04c-5f211b4eeb07", 0, "1b78684a-14c9-4914-a6af-a2befb5854b8", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "102b1048-970d-49c7-ac40-ad3f3c138e73", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9f6d573f-6a2c-4ca1-b04c-5f211b4eeb07");

            migrationBuilder.DropColumn(
                name: "Rejected",
                table: "DoctorAttachment");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "206c1816-bee7-4c00-8c5d-a8031b618ad2", 0, "6bc29f1c-4da3-420e-92d3-2f1cb6edd151", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "ce1b3f8c-45f4-4129-994f-d864fe1e1712", false, "admin" });
        }
    }
}
