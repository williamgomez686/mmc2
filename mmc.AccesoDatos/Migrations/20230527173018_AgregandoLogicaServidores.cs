using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace mmc.AccesoDatos.Migrations
{
    public partial class AgregandoLogicaServidores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IglesiaDepartamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Usuario = table.Column<string>(type: "text", nullable: true),
                    UsuarioModifica = table.Column<string>(type: "text", nullable: true),
                    FechaAlta = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Fechamodifica = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IglesiaDepartamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IglesiaReuniones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreReunion = table.Column<string>(type: "text", nullable: false),
                    ReunionFecha = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Usuario = table.Column<string>(type: "text", nullable: true),
                    UsuarioModifica = table.Column<string>(type: "text", nullable: true),
                    FechaAlta = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Fechamodifica = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IglesiaReuniones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IglesiaServidores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombres = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: true),
                    DepartamentoId = table.Column<int>(type: "integer", nullable: false),
                    Usuario = table.Column<string>(type: "text", nullable: true),
                    UsuarioModifica = table.Column<string>(type: "text", nullable: true),
                    FechaAlta = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Fechamodifica = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IglesiaServidores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IglesiaServidores_IglesiaDepartamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "IglesiaDepartamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IglesiaServidoresReuniones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Acompañantes = table.Column<int>(type: "integer", nullable: true),
                    Asiste = table.Column<bool>(type: "boolean", nullable: false),
                    ReunionId = table.Column<int>(type: "integer", nullable: false),
                    ServidorId = table.Column<int>(type: "integer", nullable: false),
                    Usuario = table.Column<string>(type: "text", nullable: true),
                    UsuarioModifica = table.Column<string>(type: "text", nullable: true),
                    FechaAlta = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Fechamodifica = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IglesiaServidoresReuniones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IglesiaServidoresReuniones_IglesiaReuniones_ReunionId",
                        column: x => x.ReunionId,
                        principalTable: "IglesiaReuniones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IglesiaServidoresReuniones_IglesiaServidores_ServidorId",
                        column: x => x.ServidorId,
                        principalTable: "IglesiaServidores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IglesiaServidores_DepartamentoId",
                table: "IglesiaServidores",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_IglesiaServidoresReuniones_ReunionId",
                table: "IglesiaServidoresReuniones",
                column: "ReunionId");

            migrationBuilder.CreateIndex(
                name: "IX_IglesiaServidoresReuniones_ServidorId",
                table: "IglesiaServidoresReuniones",
                column: "ServidorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IglesiaServidoresReuniones");

            migrationBuilder.DropTable(
                name: "IglesiaReuniones");

            migrationBuilder.DropTable(
                name: "IglesiaServidores");

            migrationBuilder.DropTable(
                name: "IglesiaDepartamentos");
        }
    }
}
