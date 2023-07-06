using Microsoft.EntityFrameworkCore.Migrations;

namespace PNT1_Grupo6.Migrations
{
    public partial class PNT1Grupo6estilosfinales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioUnitario",
                table: "OrdenesCompra",
                type: "decimal(10, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioUnitario",
                table: "OrdenesCompra",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10, 2)");
        }
    }
}
