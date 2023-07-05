using Microsoft.EntityFrameworkCore.Migrations;

namespace PNT1_Grupo6.Migrations
{
    public partial class PNT1_Grupo6ProductoPrecioadecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioVenta",
                table: "Productos",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PrecioVenta",
                table: "Productos",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
