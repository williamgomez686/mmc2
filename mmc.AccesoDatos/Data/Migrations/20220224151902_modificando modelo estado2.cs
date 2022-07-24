using Microsoft.EntityFrameworkCore.Migrations;

namespace mmc.Data.Migrations
{
    public partial class modificandomodeloestado2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagenURL",
                table: "Tickets",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "est_est",
                table: "estado",
                type: "boolean",
                maxLength: 20,
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagenURL",
                table: "Tickets");

            migrationBuilder.AlterColumn<string>(
                name: "est_est",
                table: "estado",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldMaxLength: 20);
        }
    }
}
