using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAMAMedellin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCentroCostoAndUtilidadToVentas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CentroCostoId",
                table: "Ventas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "CostoUnitario",
                table: "DetallesVenta",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Utilidad",
                table: "DetallesVenta",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_CentroCostoId",
                table: "Ventas",
                column: "CentroCostoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_CentrosCosto_CentroCostoId",
                table: "Ventas",
                column: "CentroCostoId",
                principalTable: "CentrosCosto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_CentrosCosto_CentroCostoId",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_CentroCostoId",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "CentroCostoId",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "CostoUnitario",
                table: "DetallesVenta");

            migrationBuilder.DropColumn(
                name: "Utilidad",
                table: "DetallesVenta");
        }
    }
}
