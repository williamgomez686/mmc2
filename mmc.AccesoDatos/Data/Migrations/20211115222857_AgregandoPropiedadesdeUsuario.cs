using Microsoft.EntityFrameworkCore.Migrations;

namespace mmc.Data.Migrations
{
    public partial class AgregandoPropiedadesdeUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "apellido",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "departamento",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "empresa",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "extencion",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nombre",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "apellido",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "departamento",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "empresa",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "extencion",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "nombre",
                table: "AspNetUsers");
        }
    }
}
