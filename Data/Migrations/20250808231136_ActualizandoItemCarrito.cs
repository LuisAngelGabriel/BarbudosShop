using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarbudosShop.Migrations
{
    /// <inheritdoc />
    public partial class ActualizandoItemCarrito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemsCarrito_Productos_ProductoIdProducto",
                table: "ItemsCarrito");

            migrationBuilder.DropIndex(
                name: "IX_ItemsCarrito_ProductoIdProducto",
                table: "ItemsCarrito");

            migrationBuilder.DropColumn(
                name: "ProductoIdProducto",
                table: "ItemsCarrito");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsCarrito_IdProducto",
                table: "ItemsCarrito",
                column: "IdProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsCarrito_Productos_IdProducto",
                table: "ItemsCarrito",
                column: "IdProducto",
                principalTable: "Productos",
                principalColumn: "IdProducto",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemsCarrito_Productos_IdProducto",
                table: "ItemsCarrito");

            migrationBuilder.DropIndex(
                name: "IX_ItemsCarrito_IdProducto",
                table: "ItemsCarrito");

            migrationBuilder.AddColumn<int>(
                name: "ProductoIdProducto",
                table: "ItemsCarrito",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ItemsCarrito_ProductoIdProducto",
                table: "ItemsCarrito",
                column: "ProductoIdProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsCarrito_Productos_ProductoIdProducto",
                table: "ItemsCarrito",
                column: "ProductoIdProducto",
                principalTable: "Productos",
                principalColumn: "IdProducto",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
