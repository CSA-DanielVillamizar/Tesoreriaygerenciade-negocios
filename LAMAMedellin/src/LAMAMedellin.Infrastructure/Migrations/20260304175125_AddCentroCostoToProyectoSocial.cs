using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAMAMedellin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCentroCostoToProyectoSocial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CentroCostoId",
                table: "ProyectosSociales",
                type: "uniqueidentifier",
                                nullable: true);

            migrationBuilder.Sql(
                    @"UPDATE p
                                    SET p.CentroCostoId = c.Id
                                    FROM ProyectosSociales p
                                    CROSS JOIN (
                                        SELECT TOP 1 Id
                                        FROM CentrosCosto
                                        ORDER BY Nombre
                                    ) c
                                    WHERE p.CentroCostoId IS NULL;");

            migrationBuilder.AlterColumn<Guid>(
                    name: "CentroCostoId",
                    table: "ProyectosSociales",
                    type: "uniqueidentifier",
                    nullable: false,
                    oldClrType: typeof(Guid),
                    oldType: "uniqueidentifier",
                    oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProyectosSociales_CentroCostoId",
                table: "ProyectosSociales",
                column: "CentroCostoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProyectosSociales_CentrosCosto_CentroCostoId",
                table: "ProyectosSociales",
                column: "CentroCostoId",
                principalTable: "CentrosCosto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProyectosSociales_CentrosCosto_CentroCostoId",
                table: "ProyectosSociales");

            migrationBuilder.DropIndex(
                name: "IX_ProyectosSociales_CentroCostoId",
                table: "ProyectosSociales");

            migrationBuilder.DropColumn(
                name: "CentroCostoId",
                table: "ProyectosSociales");
        }
    }
}
