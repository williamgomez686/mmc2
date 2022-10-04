using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mmc.AccesoDatos.Migrations
{
    public partial class cambiosendetalle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cistianos",
                table: "CEB_DETs",
                newName: "Cristianos");

            migrationBuilder.AlterColumn<double>(
                name: "Ofrenda",
                table: "CEB_DETs",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCEB",
                table: "CEB_DETs",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCEB",
                table: "CEB_DETs");

            migrationBuilder.RenameColumn(
                name: "Cristianos",
                table: "CEB_DETs",
                newName: "Cistianos");

            migrationBuilder.AlterColumn<float>(
                name: "Ofrenda",
                table: "CEB_DETs",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
