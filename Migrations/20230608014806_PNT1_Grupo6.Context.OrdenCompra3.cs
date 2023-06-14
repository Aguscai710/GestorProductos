using Microsoft.EntityFrameworkCore.Migrations;

namespace PNT1_Grupo6.Migrations
{
    public partial class PNT1_Grupo6ContextOrdenCompra3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoProducto",
                table: "OrdenesCompra",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreProducto",
                table: "OrdenesCompra",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoProducto",
                table: "OrdenesCompra");

            migrationBuilder.DropColumn(
                name: "NombreProducto",
                table: "OrdenesCompra");
        }
    }
}
