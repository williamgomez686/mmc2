using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace mmc.Data.Migrations
{
    public partial class AregandoTablaEstado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "estado",
                columns: table => new
                {
                    estadoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    est_descripcion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    est_est = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    est_fchalt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    est_usu_alt = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estado", x => x.estadoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "estado");
        }
    }
}
