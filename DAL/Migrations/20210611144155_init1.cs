using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorDoctorService");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1e71df9-6fdf-485a-a4be-845ab5836467");

            migrationBuilder.AddColumn<string>(
                name: "DoctorUserId",
                table: "DoctorService",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Doctor_DoctorServices",
                columns: table => new
                {
                    doctorID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    serviceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor_DoctorServices", x => new { x.doctorID, x.serviceID });
                    table.ForeignKey(
                        name: "FK_Doctor_DoctorServices_Doctor_doctorID",
                        column: x => x.doctorID,
                        principalTable: "Doctor",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctor_DoctorServices_DoctorService_serviceID",
                        column: x => x.serviceID,
                        principalTable: "DoctorService",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a4cdb9ee-d0fc-4085-ad47-45beef030a15", 0, "a24565bc-4f3f-40dd-be02-cc50bcfa6138", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "d349d38a-bab2-4e9d-8dbd-cc271e55d07d", false, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorService_DoctorUserId",
                table: "DoctorService",
                column: "DoctorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_DoctorServices_serviceID",
                table: "Doctor_DoctorServices",
                column: "serviceID");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorService_Doctor_DoctorUserId",
                table: "DoctorService",
                column: "DoctorUserId",
                principalTable: "Doctor",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorService_Doctor_DoctorUserId",
                table: "DoctorService");

            migrationBuilder.DropTable(
                name: "Doctor_DoctorServices");

            migrationBuilder.DropIndex(
                name: "IX_DoctorService_DoctorUserId",
                table: "DoctorService");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a4cdb9ee-d0fc-4085-ad47-45beef030a15");

            migrationBuilder.DropColumn(
                name: "DoctorUserId",
                table: "DoctorService");

            migrationBuilder.CreateTable(
                name: "DoctorDoctorService",
                columns: table => new
                {
                    DoctorsUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    doctorServicesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorDoctorService", x => new { x.DoctorsUserId, x.doctorServicesID });
                    table.ForeignKey(
                        name: "FK_DoctorDoctorService_Doctor_DoctorsUserId",
                        column: x => x.DoctorsUserId,
                        principalTable: "Doctor",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorDoctorService_DoctorService_doctorServicesID",
                        column: x => x.doctorServicesID,
                        principalTable: "DoctorService",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a1e71df9-6fdf-485a-a4be-845ab5836467", 0, "fdd22977-ff97-4c0e-a9b1-79bb6038e27a", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "c6602063-560b-4848-9c16-d2f54c4976af", false, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorDoctorService_doctorServicesID",
                table: "DoctorDoctorService",
                column: "doctorServicesID");
        }
    }
}
