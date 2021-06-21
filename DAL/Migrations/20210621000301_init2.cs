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
                keyValue: "1d448bc1-7c4f-49dc-a20e-fd05973f3b66");

            migrationBuilder.AddColumn<double>(
                name: "AverageRate",
                table: "Doctor",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d016ea44-8551-4a7c-b09d-df720b3f35cf", 0, "6ba6fe66-8d8d-48b4-8e06-07953217df6a", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "b78ce4ad-0ac7-43f2-9c7e-c9b939985339", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d016ea44-8551-4a7c-b09d-df720b3f35cf");

            migrationBuilder.DropColumn(
                name: "AverageRate",
                table: "Doctor");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1d448bc1-7c4f-49dc-a20e-fd05973f3b66", 0, "7eec846c-6a4a-4957-b020-282cd5702606", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "336ccf5f-da67-4a18-92c2-8c92257a1261", false, "admin" });
        }
    }
}
