using Microsoft.EntityFrameworkCore.Migrations;

namespace mmc.AccesoDatos.Migrations
{
    public partial class Cambioentablamiebros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Miembros_privilegios_cargoId",
                table: "Miembros");

            migrationBuilder.DropIndex(
                name: "IX_Miembros_cargoId",
                table: "Miembros");

            migrationBuilder.DropColumn(
                name: "cargoId",
                table: "Miembros");

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Miembros",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Miembros_CargosCEBId",
                table: "Miembros",
                column: "CargosCEBId");

            migrationBuilder.AddForeignKey(
                name: "FK_Miembros_privilegios_CargosCEBId",
                table: "Miembros",
                column: "CargosCEBId",
                principalTable: "privilegios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Miembros_privilegios_CargosCEBId",
                table: "Miembros");

            migrationBuilder.DropIndex(
                name: "IX_Miembros_CargosCEBId",
                table: "Miembros");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Miembros");

            migrationBuilder.AddColumn<int>(
                name: "cargoId",
                table: "Miembros",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Miembros_cargoId",
                table: "Miembros",
                column: "cargoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Miembros_privilegios_cargoId",
                table: "Miembros",
                column: "cargoId",
                principalTable: "privilegios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
