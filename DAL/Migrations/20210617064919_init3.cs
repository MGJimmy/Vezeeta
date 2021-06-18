using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReserveOffer_MakeOffers_OfferId",
                table: "ReserveOffer");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "602fda11-1f2d-417b-b263-a668d47c88ba");

            migrationBuilder.RenameColumn(
                name: "OfferId",
                table: "ReserveOffer",
                newName: "MakeOfferId");

            migrationBuilder.RenameIndex(
                name: "IX_ReserveOffer_OfferId",
                table: "ReserveOffer",
                newName: "IX_ReserveOffer_MakeOfferId");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3cbbb833-f476-4a3d-9ee7-80efd7eb9fd8", 0, "df32fb4c-302e-4cd0-afeb-85b3ce9123a8", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "d57ea574-bac5-45a2-bb72-566a5e1761e0", false, "admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_ReserveOffer_MakeOffers_MakeOfferId",
                table: "ReserveOffer",
                column: "MakeOfferId",
                principalTable: "MakeOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReserveOffer_MakeOffers_MakeOfferId",
                table: "ReserveOffer");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3cbbb833-f476-4a3d-9ee7-80efd7eb9fd8");

            migrationBuilder.RenameColumn(
                name: "MakeOfferId",
                table: "ReserveOffer",
                newName: "OfferId");

            migrationBuilder.RenameIndex(
                name: "IX_ReserveOffer_MakeOfferId",
                table: "ReserveOffer",
                newName: "IX_ReserveOffer_OfferId");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsDoctor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "602fda11-1f2d-417b-b263-a668d47c88ba", 0, "ebb03d65-3d90-4150-9aa1-14c717f1361c", "example.gmail.com", false, null, false, false, null, null, null, "123456", null, false, "5d2291cd-41ef-4509-bc8e-d0a6985dcf8d", false, "admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_ReserveOffer_MakeOffers_OfferId",
                table: "ReserveOffer",
                column: "OfferId",
                principalTable: "MakeOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
