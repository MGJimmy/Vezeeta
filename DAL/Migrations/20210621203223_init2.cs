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
                keyValue: "4fb1f113-618f-44ae-af0f-78ea58859d25");

            migrationBuilder.AddColumn<bool>(
                name: "IsRated",
                table: "ReserveOffer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "311cf904-3b29-458e-828b-00529109db6c", 0, "68d43d01-4255-443f-92db-36ce28d771bb", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "71599594-5090-4866-b2f1-e70627ef0eac", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "311cf904-3b29-458e-828b-00529109db6c");

            migrationBuilder.DropColumn(
                name: "IsRated",
                table: "ReserveOffer");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4fb1f113-618f-44ae-af0f-78ea58859d25", 0, "fd700424-4e1a-4d90-8d66-2fda637ddaa7", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "b3bacf8a-ebd5-4b74-aa89-14403e26bc3e", false, "admin" });
        }
    }
}
