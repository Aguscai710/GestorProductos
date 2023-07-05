using Microsoft.EntityFrameworkCore.Migrations;

namespace PNT1_Grupo6.Migrations
{
    public partial class PNT1_Grupo6OrdenesCompraRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoProducto",
                table: "OrdenesCompra");

            migrationBuilder.DropColumn(
                name: "CodigoProveedor",
                table: "OrdenesCompra");

            migrationBuilder.DropColumn(
                name: "NombreProducto",
                table: "OrdenesCompra");

            migrationBuilder.DropColumn(
                name: "NombreProveedor",
                table: "OrdenesCompra");

            migrationBuilder.DropColumn(
                name: "NumeroOrden",
                table: "OrdenesCompra");

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioVenta",
                table: "Productos",
                type: "decimal(10, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioUnitario",
                table: "OrdenesCompra",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "OrdenesCompra",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProveedorId",
                table: "OrdenesCompra",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "OrdenesCompra");

            migrationBuilder.DropColumn(
                name: "ProveedorId",
                table: "OrdenesCompra");

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioVenta",
                table: "Productos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10, 2)");

            migrationBuilder.AlterColumn<double>(
                name: "PrecioUnitario",
                table: "OrdenesCompra",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<string>(
                name: "CodigoProducto",
                table: "OrdenesCompra",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CodigoProveedor",
                table: "OrdenesCompra",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NombreProducto",
                table: "OrdenesCompra",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreProveedor",
                table: "OrdenesCompra",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroOrden",
                table: "OrdenesCompra",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
