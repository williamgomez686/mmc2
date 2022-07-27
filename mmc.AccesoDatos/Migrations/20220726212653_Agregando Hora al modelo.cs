using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mmc.AccesoDatos.Migrations
{
    public partial class AgregandoHoraalmodelo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Hora",
                table: "CasasEstudioBiblico",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hora",
                table: "CasasEstudioBiblico");
        }
    }
}
