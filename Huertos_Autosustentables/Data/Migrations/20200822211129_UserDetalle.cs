using Microsoft.EntityFrameworkCore.Migrations;

namespace Huertos_Autosustentables.Data.Migrations
{
    public partial class UserDetalle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "DetalleUsersCultivo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetalleUsersCultivo_UserId",
                table: "DetalleUsersCultivo",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleUsersCultivo_AspNetUsers_UserId",
                table: "DetalleUsersCultivo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleUsersCultivo_AspNetUsers_UserId",
                table: "DetalleUsersCultivo");

            migrationBuilder.DropIndex(
                name: "IX_DetalleUsersCultivo_UserId",
                table: "DetalleUsersCultivo");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DetalleUsersCultivo");
        }
    }
}
