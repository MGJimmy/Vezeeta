using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorSupSpecialization");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b898f42a-4bf7-44f4-9042-a4c091ebe1fb");

            migrationBuilder.AddColumn<int>(
                name: "MaxNumOfReservation",
                table: "DayShift",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ByAdmin",
                table: "Clinicservices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ClinicClinicService",
                columns: table => new
                {
                    ClinicId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClinicServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicClinicService", x => new { x.ClinicId, x.ClinicServiceId });
                    table.ForeignKey(
                        name: "FK_ClinicClinicService_Clinic_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinic",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClinicClinicService_Clinicservices_ClinicServiceId",
                        column: x => x.ClinicServiceId,
                        principalTable: "Clinicservices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorSubSpecialization",
                columns: table => new
                {
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    subSpecializeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSubSpecialization", x => new { x.DoctorId, x.subSpecializeId });
                    table.ForeignKey(
                        name: "FK_DoctorSubSpecialization_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorSubSpecialization_supSpecializations_subSpecializeId",
                        column: x => x.subSpecializeId,
                        principalTable: "supSpecializations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5b450e8e-1394-4d5f-a6da-5515658fcd24", 0, "2de455ab-d7d6-44f5-9b86-90bc7a437edb", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "f9ae829e-5211-4089-aef9-847689a45ef9", false, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_ClinicClinicService_ClinicServiceId",
                table: "ClinicClinicService",
                column: "ClinicServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSubSpecialization_subSpecializeId",
                table: "DoctorSubSpecialization",
                column: "subSpecializeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClinicClinicService");

            migrationBuilder.DropTable(
                name: "DoctorSubSpecialization");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5b450e8e-1394-4d5f-a6da-5515658fcd24");

            migrationBuilder.DropColumn(
                name: "MaxNumOfReservation",
                table: "DayShift");

            migrationBuilder.DropColumn(
                name: "ByAdmin",
                table: "Clinicservices");

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
                values: new object[] { "b898f42a-4bf7-44f4-9042-a4c091ebe1fb", 0, "d38568ce-019c-4702-8da0-35d6fc84ea0b", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "79376bc4-d017-4dab-bf46-bdd9cb294b19", false, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSupSpecialization_supSpecializationsID",
                table: "DoctorSupSpecialization",
                column: "supSpecializationsID");
        }
    }
}
