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
                keyValue: "261e218b-7959-4690-963a-f5740ba605c6");

            migrationBuilder.AddColumn<int>(
                name: "specialtyId",
                table: "Doctor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DoctorSupSpecialization",
                columns: table => new
                {
                    doctorsUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    supSpecializationsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSupSpecialization", x => new { x.doctorsUserId, x.supSpecializationsID });
                    table.ForeignKey(
                        name: "FK_DoctorSupSpecialization_Doctor_doctorsUserId",
                        column: x => x.doctorsUserId,
                        principalTable: "Doctor",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorSupSpecialization_supSpecializations_supSpecializationsID",
                        column: x => x.supSpecializationsID,
                        principalTable: "supSpecializations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8c456b31-f4af-424d-b31f-af3db48d07b8", 0, "718b8182-5fb0-4926-800a-b8c159f94569", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "a55c63b3-0238-4a50-ae97-c38632530cf5", false, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_specialtyId",
                table: "Doctor",
                column: "specialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSupSpecialization_supSpecializationsID",
                table: "DoctorSupSpecialization",
                column: "supSpecializationsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Specialty_specialtyId",
                table: "Doctor",
                column: "specialtyId",
                principalTable: "Specialty",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Specialty_specialtyId",
                table: "Doctor");

            migrationBuilder.DropTable(
                name: "DoctorSupSpecialization");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_specialtyId",
                table: "Doctor");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8c456b31-f4af-424d-b31f-af3db48d07b8");

            migrationBuilder.DropColumn(
                name: "specialtyId",
                table: "Doctor");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "261e218b-7959-4690-963a-f5740ba605c6", 0, "4a1808ab-9dfc-4fab-b666-5955f87660ba", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "2b3c705d-c07d-418d-8f60-528823458739", false, "admin" });
        }
    }
}
