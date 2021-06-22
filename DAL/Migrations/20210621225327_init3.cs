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
                keyValue: "311cf904-3b29-458e-828b-00529109db6c");

            migrationBuilder.AddColumn<bool>(
                name: "IsRated",
                table: "Reservation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "206c1816-bee7-4c00-8c5d-a8031b618ad2", 0, "6bc29f1c-4da3-420e-92d3-2f1cb6edd151", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "ce1b3f8c-45f4-4129-994f-d864fe1e1712", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "206c1816-bee7-4c00-8c5d-a8031b618ad2");

            migrationBuilder.DropColumn(
                name: "IsRated",
                table: "Reservation");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "311cf904-3b29-458e-828b-00529109db6c", 0, "68d43d01-4255-443f-92db-36ce28d771bb", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "71599594-5090-4866-b2f1-e70627ef0eac", false, "admin" });
        }
    }
}
