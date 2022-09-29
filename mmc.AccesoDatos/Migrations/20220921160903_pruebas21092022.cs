using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace mmc.AccesoDatos.Migrations
{
    public partial class pruebas21092022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CEB_CABs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Hora = table.Column<string>(type: "text", nullable: true),
                    TipoCebId = table.Column<int>(type: "integer", nullable: false),
                    MiembrosCEBid = table.Column<int>(type: "integer", nullable: false),
                    Ceb_Detalle = table.Column<int>(type: "integer", nullable: false),
                    Usuario = table.Column<string>(type: "text", nullable: true),
                    UsuarioModifica = table.Column<string>(type: "text", nullable: true),
                    FechaAlta = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Fechamodifica = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CEB_CABs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CEB_CABs_Miembros_MiembrosCEBid",
                        column: x => x.MiembrosCEBid,
                        principalTable: "Miembros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CEB_CABs_TiposCEB_TipoCebId",
                        column: x => x.TipoCebId,
                        principalTable: "TiposCEB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CEB_DETs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cistianos = table.Column<int>(type: "integer", nullable: false),
                    NoCistianos = table.Column<int>(type: "integer", nullable: false),
                    Ninios = table.Column<int>(type: "integer", nullable: false),
                    Total = table.Column<int>(type: "integer", nullable: false),
                    Convertidos = table.Column<int>(type: "integer", nullable: false),
                    Reconcilia = table.Column<int>(type: "integer", nullable: false),
                    Ofrenda = table.Column<float>(type: "real", nullable: false),
                    Foto = table.Column<string>(type: "text", nullable: true),
                    Ceb_Detalle = table.Column<int>(type: "integer", nullable: true),
                    Usuario = table.Column<string>(type: "text", nullable: true),
                    UsuarioModifica = table.Column<string>(type: "text", nullable: true),
                    FechaAlta = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Fechamodifica = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CEB_DETs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CEB_DETs_CEB_CABs_Ceb_Detalle",
                        column: x => x.Ceb_Detalle,
                        principalTable: "CEB_CABs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CEB_CABs_MiembrosCEBid",
                table: "CEB_CABs",
                column: "MiembrosCEBid");

            migrationBuilder.CreateIndex(
                name: "IX_CEB_CABs_TipoCebId",
                table: "CEB_CABs",
                column: "TipoCebId");

            migrationBuilder.CreateIndex(
                name: "IX_CEB_DETs_Ceb_Detalle",
                table: "CEB_DETs",
                column: "Ceb_Detalle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CEB_DETs");

            migrationBuilder.DropTable(
                name: "CEB_CABs");
        }
    }
}
