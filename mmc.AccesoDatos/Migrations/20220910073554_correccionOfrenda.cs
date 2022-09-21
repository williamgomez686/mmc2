using Microsoft.EntityFrameworkCore.Migrations;

namespace mmc.AccesoDatos.Migrations
{
    public partial class correccionOfrenda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Ofrenda",
                table: "CasasEstudioBiblico",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Ofrenda",
                table: "CasasEstudioBiblico",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }
    }
}
