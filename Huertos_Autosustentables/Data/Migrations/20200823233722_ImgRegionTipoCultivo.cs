using Microsoft.EntityFrameworkCore.Migrations;

namespace Huertos_Autosustentables.Data.Migrations
{
    public partial class ImgRegionTipoCultivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "TipoCultivo",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Region",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Cultivo",
                type: "nvarchar(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "TipoCultivo");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Region");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Cultivo");
        }
    }
}
