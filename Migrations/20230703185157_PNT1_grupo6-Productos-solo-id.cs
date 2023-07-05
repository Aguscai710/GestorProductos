using Microsoft.EntityFrameworkCore.Migrations;

namespace PNT1_Grupo6.Migrations
{
    public partial class PNT1_grupo6Productossoloid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoProducto",
                table: "Productos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoProducto",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
