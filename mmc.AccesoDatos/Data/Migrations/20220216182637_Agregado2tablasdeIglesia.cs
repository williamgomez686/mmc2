using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace mmc.Data.Migrations
{
    public partial class Agregado2tablasdeIglesia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AsistenciaMiembros",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NumeroContacto = table.Column<int>(type: "integer", nullable: false),
                    MiembroFamilia = table.Column<string>(type: "text", nullable: false),
                    Cantidad = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsistenciaMiembros", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ServiciosIglesia",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    Servicio = table.Column<string>(type: "text", nullable: false),
                    Horario = table.Column<int>(type: "integer", nullable: false),
                    AsistenciaMiembrosid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiciosIglesia", x => x.id);
                    table.ForeignKey(
                        name: "FK_ServiciosIglesia_AsistenciaMiembros_AsistenciaMiembrosid",
                        column: x => x.AsistenciaMiembrosid,
                        principalTable: "AsistenciaMiembros",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiciosIglesia_AsistenciaMiembrosid",
                table: "ServiciosIglesia",
                column: "AsistenciaMiembrosid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiciosIglesia");

            migrationBuilder.DropTable(
                name: "AsistenciaMiembros");
        }
    }
}
