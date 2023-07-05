using Microsoft.EntityFrameworkCore.Migrations;

namespace PNT1_Grupo6.Migrations
{
    public partial class PNT1_Grupo6Proveedoraid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoProveedor",
                table: "Proveedores");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoProveedor",
                table: "Proveedores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
