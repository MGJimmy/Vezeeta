using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class addClinic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e472bcb5-5340-48e1-bdae-0395c95f18b1");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0134f38f-71be-4929-8a7e-e34f76240147", 0, "d9f28e97-cc06-4d19-9a23-0b614b8e1db2", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "4ec8153a-2517-434c-a07a-38146d5f2b29", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0134f38f-71be-4929-8a7e-e34f76240147");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e472bcb5-5340-48e1-bdae-0395c95f18b1", 0, "86c70bb5-c740-4915-90ba-455e51ec008d", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "505ad884-7717-484a-bcc5-c0e816cae388", false, "admin" });
        }
    }
}
