using Microsoft.EntityFrameworkCore.Migrations;

namespace mmc.AccesoDatos.Migrations
{
    public partial class preubaconcabydetenceb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CEB_DETs_CEB_CABs_Ceb_Detalle",
                table: "CEB_DETs");

            migrationBuilder.DropIndex(
                name: "IX_CEB_DETs_Ceb_Detalle",
                table: "CEB_DETs");

            migrationBuilder.DropColumn(
                name: "Ceb_Detalle",
                table: "CEB_DETs");

            migrationBuilder.DropColumn(
                name: "Ceb_Detalle",
                table: "CEB_CABs");

            migrationBuilder.AddColumn<int>(
                name: "CEBid",
                table: "CEB_DETs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "CEB_CABs",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CEB_DETs_CEBid",
                table: "CEB_DETs",
                column: "CEBid");

            migrationBuilder.AddForeignKey(
                name: "FK_CEB_DETs_CEB_CABs_CEBid",
                table: "CEB_DETs",
                column: "CEBid",
                principalTable: "CEB_CABs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CEB_DETs_CEB_CABs_CEBid",
                table: "CEB_DETs");

            migrationBuilder.DropIndex(
                name: "IX_CEB_DETs_CEBid",
                table: "CEB_DETs");

            migrationBuilder.DropColumn(
                name: "CEBid",
                table: "CEB_DETs");

            migrationBuilder.DropColumn(
                name: "Foto",
                table: "CEB_CABs");

            migrationBuilder.AddColumn<int>(
                name: "Ceb_Detalle",
                table: "CEB_DETs",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ceb_Detalle",
                table: "CEB_CABs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CEB_DETs_Ceb_Detalle",
                table: "CEB_DETs",
                column: "Ceb_Detalle");

            migrationBuilder.AddForeignKey(
                name: "FK_CEB_DETs_CEB_CABs_Ceb_Detalle",
                table: "CEB_DETs",
                column: "Ceb_Detalle",
                principalTable: "CEB_CABs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
