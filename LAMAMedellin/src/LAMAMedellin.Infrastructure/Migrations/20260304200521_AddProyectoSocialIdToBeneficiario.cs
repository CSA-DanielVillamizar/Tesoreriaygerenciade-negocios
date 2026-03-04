using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAMAMedellin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProyectoSocialIdToBeneficiario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProyectoSocialId",
                table: "Beneficiarios",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.Sql(
                """
                DECLARE @ProyectoSocialId uniqueidentifier = (SELECT TOP 1 Id FROM ProyectosSociales ORDER BY FechaCreacion);
                IF @ProyectoSocialId IS NULL
                BEGIN
                    THROW 50000, 'No existen ProyectosSociales para asociar beneficiarios existentes.', 1;
                END;

                UPDATE Beneficiarios
                SET ProyectoSocialId = @ProyectoSocialId
                WHERE ProyectoSocialId IS NULL;
                """);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProyectoSocialId",
                table: "Beneficiarios",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiarios_ProyectoSocialId",
                table: "Beneficiarios",
                column: "ProyectoSocialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiarios_ProyectosSociales_ProyectoSocialId",
                table: "Beneficiarios",
                column: "ProyectoSocialId",
                principalTable: "ProyectosSociales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiarios_ProyectosSociales_ProyectoSocialId",
                table: "Beneficiarios");

            migrationBuilder.DropIndex(
                name: "IX_Beneficiarios_ProyectoSocialId",
                table: "Beneficiarios");

            migrationBuilder.DropColumn(
                name: "ProyectoSocialId",
                table: "Beneficiarios");
        }
    }
}
