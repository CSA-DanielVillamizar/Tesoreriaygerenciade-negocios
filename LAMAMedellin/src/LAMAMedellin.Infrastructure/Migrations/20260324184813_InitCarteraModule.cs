using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAMAMedellin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitCarteraModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apodo",
                table: "Miembros",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DocumentoIdentidad",
                table: "Miembros",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaIngreso",
                table: "Miembros",
                type: "date",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<string>(
                name: "Nombres",
                table: "Miembros",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TipoMiembro",
                table: "Miembros",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<Guid>(
                name: "ConceptoCobroId",
                table: "CuentasPorCobrar",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaEmision",
                table: "CuentasPorCobrar",
                type: "date",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaVencimiento",
                table: "CuentasPorCobrar",
                type: "date",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<decimal>(
                name: "SaldoPendiente",
                table: "CuentasPorCobrar",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorTotal",
                table: "CuentasPorCobrar",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "ConceptosCobro",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ValorCOP = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PeriodicidadMensual = table.Column<int>(type: "int", nullable: false),
                    CuentaContableIngresoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptosCobro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConceptosCobro_CuentasContables_CuentaContableIngresoId",
                        column: x => x.CuentaContableIngresoId,
                        principalTable: "CuentasContables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConceptosCobro_CuentaContableIngresoId",
                table: "ConceptosCobro",
                column: "CuentaContableIngresoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConceptosCobro_Nombre",
                table: "ConceptosCobro",
                column: "Nombre",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConceptosCobro");

            migrationBuilder.DropColumn(
                name: "Apodo",
                table: "Miembros");

            migrationBuilder.DropColumn(
                name: "DocumentoIdentidad",
                table: "Miembros");

            migrationBuilder.DropColumn(
                name: "FechaIngreso",
                table: "Miembros");

            migrationBuilder.DropColumn(
                name: "Nombres",
                table: "Miembros");

            migrationBuilder.DropColumn(
                name: "TipoMiembro",
                table: "Miembros");

            migrationBuilder.DropColumn(
                name: "ConceptoCobroId",
                table: "CuentasPorCobrar");

            migrationBuilder.DropColumn(
                name: "FechaEmision",
                table: "CuentasPorCobrar");

            migrationBuilder.DropColumn(
                name: "FechaVencimiento",
                table: "CuentasPorCobrar");

            migrationBuilder.DropColumn(
                name: "SaldoPendiente",
                table: "CuentasPorCobrar");

            migrationBuilder.DropColumn(
                name: "ValorTotal",
                table: "CuentasPorCobrar");
        }
    }
}
