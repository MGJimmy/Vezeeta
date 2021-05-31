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
                keyValue: "71951a1d-8941-4214-ba6c-487b5ddd8705");

            migrationBuilder.AddColumn<bool>(
                name: "isBinding",
                table: "DoctorAttachment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3a53fd98-5459-4fc2-b2e7-eefebc129dd8", 0, "f6bc8326-3947-4703-9484-704f925374ea", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "778c98d9-975e-429e-b97b-ca642d70a962", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3a53fd98-5459-4fc2-b2e7-eefebc129dd8");

            migrationBuilder.DropColumn(
                name: "isBinding",
                table: "DoctorAttachment");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "71951a1d-8941-4214-ba6c-487b5ddd8705", 0, "8502c2e6-0ded-41d5-bb64-8cc4739f2eeb", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "9dfa780c-2612-486e-b747-011d343e51e7", false, "admin" });
        }
    }
}
