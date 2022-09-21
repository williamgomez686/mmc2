using Microsoft.EntityFrameworkCore.Migrations;

namespace mmc.AccesoDatos.Migrations
{
    public partial class correccionOfrenda2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Ofrenda",
                table: "CasasEstudioBiblico",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Ofrenda",
                table: "CasasEstudioBiblico",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
