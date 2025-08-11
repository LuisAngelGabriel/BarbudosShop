﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarbudosShop.Migrations
{
    /// <inheritdoc />
    public partial class AddCantidadToCarrito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "ItemsCarrito",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "ItemsCarrito");
        }
    }
}
