using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace mmc.AccesoDatos.Migrations
{
    public partial class AgregandologicadeTickets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AreaSoporteTK",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UsuarioAlta = table.Column<string>(type: "text", nullable: true),
                    UsuarioModifica = table.Column<string>(type: "text", nullable: true),
                    FechaAlta = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Fechamodifica = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaSoporteTK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmpresasTK",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UsuarioAlta = table.Column<string>(type: "text", nullable: true),
                    UsuarioModifica = table.Column<string>(type: "text", nullable: true),
                    FechaAlta = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Fechamodifica = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresasTK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadosTKs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UsuarioAlta = table.Column<string>(type: "text", nullable: true),
                    UsuarioModifica = table.Column<string>(type: "text", nullable: true),
                    FechaAlta = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Fechamodifica = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosTKs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UrgenciasTK",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UsuarioAlta = table.Column<string>(type: "text", nullable: true),
                    UsuarioModifica = table.Column<string>(type: "text", nullable: true),
                    FechaAlta = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Fechamodifica = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrgenciasTK", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Usuario = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Asunto = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Solucion = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    FechaSolucion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Tecnico = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ImagenUrl = table.Column<string>(type: "text", nullable: true),
                    EstadoTKId = table.Column<int>(type: "integer", nullable: false),
                    UrgenciaId = table.Column<int>(type: "integer", nullable: false),
                    AreaSoporteId = table.Column<int>(type: "integer", nullable: false),
                    SedeId = table.Column<int>(type: "integer", nullable: false),
                    UsuarioAlta = table.Column<string>(type: "text", nullable: true),
                    UsuarioModifica = table.Column<string>(type: "text", nullable: true),
                    FechaAlta = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Fechamodifica = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_AreaSoporteTK_AreaSoporteId",
                        column: x => x.AreaSoporteId,
                        principalTable: "AreaSoporteTK",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_EmpresasTK_SedeId",
                        column: x => x.SedeId,
                        principalTable: "EmpresasTK",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_EstadosTKs_EstadoTKId",
                        column: x => x.EstadoTKId,
                        principalTable: "EstadosTKs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_UrgenciasTK_UrgenciaId",
                        column: x => x.UrgenciaId,
                        principalTable: "UrgenciasTK",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AreaSoporteId",
                table: "Tickets",
                column: "AreaSoporteId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EstadoTKId",
                table: "Tickets",
                column: "EstadoTKId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SedeId",
                table: "Tickets",
                column: "SedeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UrgenciaId",
                table: "Tickets",
                column: "UrgenciaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "AreaSoporteTK");

            migrationBuilder.DropTable(
                name: "EmpresasTK");

            migrationBuilder.DropTable(
                name: "EstadosTKs");

            migrationBuilder.DropTable(
                name: "UrgenciasTK");
        }
    }
}
