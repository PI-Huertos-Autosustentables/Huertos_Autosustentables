using Microsoft.EntityFrameworkCore.Migrations;

namespace Huertos_Autosustentables.Data.Migrations
{
    public partial class ImgClima : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Clima",
                type: "nvarchar(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Clima");
        }
    }
}
