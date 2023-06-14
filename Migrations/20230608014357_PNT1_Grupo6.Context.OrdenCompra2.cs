using Microsoft.EntityFrameworkCore.Migrations;

namespace PNT1_Grupo6.Migrations
{
    public partial class PNT1_Grupo6ContextOrdenCompra2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rubro",
                table: "OrdenesCompra");

            migrationBuilder.AddColumn<string>(
                name: "CodigoProveedor",
                table: "OrdenesCompra",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreProveedor",
                table: "OrdenesCompra",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoProveedor",
                table: "OrdenesCompra");

            migrationBuilder.DropColumn(
                name: "NombreProveedor",
                table: "OrdenesCompra");

            migrationBuilder.AddColumn<string>(
                name: "Rubro",
                table: "OrdenesCompra",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
