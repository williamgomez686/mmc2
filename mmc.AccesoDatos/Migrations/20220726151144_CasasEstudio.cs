using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace mmc.AccesoDatos.Migrations
{
    public partial class CasasEstudio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CasasEstudioBiblico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fecha = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TotalCristianos = table.Column<int>(type: "integer", nullable: false),
                    NoCristianos = table.Column<int>(type: "integer", nullable: false),
                    Ninos = table.Column<int>(type: "integer", nullable: false),
                    total = table.Column<int>(type: "integer", nullable: false),
                    Convertidos = table.Column<int>(type: "integer", nullable: false),
                    Reconciliados = table.Column<int>(type: "integer", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    Ofrenda = table.Column<float>(type: "real", nullable: false),
                    ImagenUrl = table.Column<string>(type: "text", nullable: true),
                    TipoCebId = table.Column<int>(type: "integer", nullable: false),
                    MiembrosCEBid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CasasEstudioBiblico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CasasEstudioBiblico_Miembros_MiembrosCEBid",
                        column: x => x.MiembrosCEBid,
                        principalTable: "Miembros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CasasEstudioBiblico_TiposCEB_TipoCebId",
                        column: x => x.TipoCebId,
                        principalTable: "TiposCEB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CasasEstudioBiblico_MiembrosCEBid",
                table: "CasasEstudioBiblico",
                column: "MiembrosCEBid");

            migrationBuilder.CreateIndex(
                name: "IX_CasasEstudioBiblico_TipoCebId",
                table: "CasasEstudioBiblico",
                column: "TipoCebId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CasasEstudioBiblico");
        }
    }
}
