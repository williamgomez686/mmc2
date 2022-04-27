using Microsoft.EntityFrameworkCore.Migrations;

namespace mmc.Data.Migrations
{
    public partial class ModifiacRelacionEntreTablas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiciosIglesia_AsistenciaMiembros_AsistenciaMiembrosid",
                table: "ServiciosIglesia");

            migrationBuilder.DropIndex(
                name: "IX_ServiciosIglesia_AsistenciaMiembrosid",
                table: "ServiciosIglesia");

            migrationBuilder.DropColumn(
                name: "AsistenciaMiembrosid",
                table: "ServiciosIglesia");

            migrationBuilder.AddColumn<string>(
                name: "horaid",
                table: "AsistenciaMiembros",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AsistenciaMiembros_horaid",
                table: "AsistenciaMiembros",
                column: "horaid");

            migrationBuilder.AddForeignKey(
                name: "FK_AsistenciaMiembros_ServiciosIglesia_horaid",
                table: "AsistenciaMiembros",
                column: "horaid",
                principalTable: "ServiciosIglesia",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsistenciaMiembros_ServiciosIglesia_horaid",
                table: "AsistenciaMiembros");

            migrationBuilder.DropIndex(
                name: "IX_AsistenciaMiembros_horaid",
                table: "AsistenciaMiembros");

            migrationBuilder.DropColumn(
                name: "horaid",
                table: "AsistenciaMiembros");

            migrationBuilder.AddColumn<int>(
                name: "AsistenciaMiembrosid",
                table: "ServiciosIglesia",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiciosIglesia_AsistenciaMiembrosid",
                table: "ServiciosIglesia",
                column: "AsistenciaMiembrosid");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiciosIglesia_AsistenciaMiembros_AsistenciaMiembrosid",
                table: "ServiciosIglesia",
                column: "AsistenciaMiembrosid",
                principalTable: "AsistenciaMiembros",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
