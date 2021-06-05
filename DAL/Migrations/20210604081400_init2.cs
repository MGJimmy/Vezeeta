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
                keyValue: "48b6e37a-302c-4748-9408-6d3e3a22611b");

            migrationBuilder.CreateTable(
                name: "DoctorSercive",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ByAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSercive", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DoctorDoctorSercive",
                columns: table => new
                {
                    DoctorSercivesID = table.Column<int>(type: "int", nullable: false),
                    DoctorsUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorDoctorSercive", x => new { x.DoctorSercivesID, x.DoctorsUserId });
                    table.ForeignKey(
                        name: "FK_DoctorDoctorSercive_Doctor_DoctorsUserId",
                        column: x => x.DoctorsUserId,
                        principalTable: "Doctor",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorDoctorSercive_DoctorSercive_DoctorSercivesID",
                        column: x => x.DoctorSercivesID,
                        principalTable: "DoctorSercive",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3a9b9719-2d12-475c-b69e-ec56ae9bbfc4", 0, "7770036a-75ab-4ec9-8d77-8f867d867e38", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "14375851-8c90-4646-a1b7-e90415081b64", false, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorDoctorSercive_DoctorsUserId",
                table: "DoctorDoctorSercive",
                column: "DoctorsUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorDoctorSercive");

            migrationBuilder.DropTable(
                name: "DoctorSercive");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3a9b9719-2d12-475c-b69e-ec56ae9bbfc4");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "48b6e37a-302c-4748-9408-6d3e3a22611b", 0, "c15b6d03-f634-49cd-a245-7732223d5c71", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "87de51e4-b2e6-4fe6-8a9a-c278a6d57a37", false, "admin" });
        }
    }
}
