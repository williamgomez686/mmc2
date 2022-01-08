using Microsoft.EntityFrameworkCore.Migrations;

namespace mmc.Data.Migrations
{
    public partial class cambioenModeloEstado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "estadoId",
                table: "estado",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "estado",
                newName: "estadoId");
        }
    }
}
