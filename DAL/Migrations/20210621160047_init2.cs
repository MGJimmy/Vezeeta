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
                keyValue: "f0eaf087-b8e8-49ff-b53d-d706cf8361c3");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Specialty",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AverageRate",
                table: "Doctor",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

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
                values: new object[] { "ba4bdfd6-abc9-4a75-9df4-a854f0789465", 0, "242d4899-6e52-41e5-8952-9ccfc1c53868", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "bd924ff6-7fec-4da7-9f6f-c41fcff66622", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ba4bdfd6-abc9-4a75-9df4-a854f0789465");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Specialty");

            migrationBuilder.DropColumn(
                name: "AverageRate",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "CountOfRating",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "SumOfRating",
                table: "Doctor");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f0eaf087-b8e8-49ff-b53d-d706cf8361c3", 0, "8bfe5e65-ead7-4e4b-9db3-14150f45ed2c", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "da43e0ec-7d3d-4ac1-918f-24396c5a580e", false, "admin" });
        }
    }
}
