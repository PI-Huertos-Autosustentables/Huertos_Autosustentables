using Microsoft.EntityFrameworkCore.Migrations;

namespace Huertos_Autosustentables.Data.Migrations
{
    public partial class FullDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clima",
                columns: table => new
                {
                    IdClima = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreClima = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    CaracteristicasClima = table.Column<string>(type: "nvarchar(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clima", x => x.IdClima);
                });

            migrationBuilder.CreateTable(
                name: "TipoCultivo",
                columns: table => new
                {
                    IdTipoCultivo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreTipoCultivos = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CaracteristicasTipoCultivos = table.Column<string>(type: "nvarchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCultivo", x => x.IdTipoCultivo);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    IdRegiones = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreRegiones = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CaracterisitcasRegiones = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    IdClima = table.Column<int>(nullable: false),
                    FK_IdClima = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.IdRegiones);
                    table.ForeignKey(
                        name: "FK_Region_Clima_FK_IdClima",
                        column: x => x.FK_IdClima,
                        principalTable: "Clima",
                        principalColumn: "IdClima",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cultivo",
                columns: table => new
                {
                    IdCultivos = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCultivos = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IntroduccionCultivos = table.Column<string>(type: "nvarchar(1500)", nullable: false),
                    CuerpoCultivos = table.Column<string>(type: "nvarchar(4000)", nullable: false),
                    RecomendacionesCultivos = table.Column<string>(type: "nvarchar(2000)", nullable: false),
                    IdTipoCultivo = table.Column<int>(nullable: false),
                    Fk_IdTipoCultivo = table.Column<int>(nullable: true),
                    IdRegiones = table.Column<int>(nullable: false),
                    FK_IdRegiones = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cultivo", x => x.IdCultivos);
                    table.ForeignKey(
                        name: "FK_Cultivo_Region_FK_IdRegiones",
                        column: x => x.FK_IdRegiones,
                        principalTable: "Region",
                        principalColumn: "IdRegiones",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cultivo_TipoCultivo_Fk_IdTipoCultivo",
                        column: x => x.Fk_IdTipoCultivo,
                        principalTable: "TipoCultivo",
                        principalColumn: "IdTipoCultivo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetalleUsersCultivo",
                columns: table => new
                {
                    IdDetalle = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MetrosCuadradosDetalle = table.Column<float>(nullable: false),
                    PrecioSemillasDetalle = table.Column<float>(nullable: false),
                    LugarCultivoDetalle = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    IdCultivo = table.Column<int>(nullable: false),
                    FK_IdCultivos = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleUsersCultivo", x => x.IdDetalle);
                    table.ForeignKey(
                        name: "FK_DetalleUsersCultivo_Cultivo_FK_IdCultivos",
                        column: x => x.FK_IdCultivos,
                        principalTable: "Cultivo",
                        principalColumn: "IdCultivos",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cultivo_FK_IdRegiones",
                table: "Cultivo",
                column: "FK_IdRegiones");

            migrationBuilder.CreateIndex(
                name: "IX_Cultivo_Fk_IdTipoCultivo",
                table: "Cultivo",
                column: "Fk_IdTipoCultivo");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleUsersCultivo_FK_IdCultivos",
                table: "DetalleUsersCultivo",
                column: "FK_IdCultivos");

            migrationBuilder.CreateIndex(
                name: "IX_Region_FK_IdClima",
                table: "Region",
                column: "FK_IdClima");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleUsersCultivo");

            migrationBuilder.DropTable(
                name: "Cultivo");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "TipoCultivo");

            migrationBuilder.DropTable(
                name: "Clima");
        }
    }
}
