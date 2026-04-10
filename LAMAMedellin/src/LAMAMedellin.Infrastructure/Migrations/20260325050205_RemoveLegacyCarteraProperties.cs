using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAMAMedellin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLegacyCarteraProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CuentasPorCobrar_MiembroId_Periodo",
                table: "CuentasPorCobrar");

            // Preserve legacy rows by replacing empty ConceptoCobroId with the row Id
            // before enforcing the new unique index.
            migrationBuilder.Sql(@"
UPDATE CuentasPorCobrar
SET ConceptoCobroId = Id
WHERE ConceptoCobroId = '00000000-0000-0000-0000-000000000000';");

            migrationBuilder.DropColumn(
                name: "Periodo",
                table: "CuentasPorCobrar");

            migrationBuilder.DropColumn(
                name: "SaldoPendienteCOP",
                table: "CuentasPorCobrar");

            migrationBuilder.DropColumn(
                name: "ValorEsperadoCOP",
                table: "CuentasPorCobrar");

            migrationBuilder.CreateIndex(
                name: "IX_CuentasPorCobrar_MiembroConceptoFecha",
                table: "CuentasPorCobrar",
                columns: new[] { "MiembroId", "ConceptoCobroId", "FechaEmision" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CuentasPorCobrar_MiembroConceptoFecha",
                table: "CuentasPorCobrar");

            migrationBuilder.AddColumn<string>(
                name: "Periodo",
                table: "CuentasPorCobrar",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "SaldoPendienteCOP",
                table: "CuentasPorCobrar",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorEsperadoCOP",
                table: "CuentasPorCobrar",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_CuentasPorCobrar_MiembroId_Periodo",
                table: "CuentasPorCobrar",
                columns: new[] { "MiembroId", "Periodo" },
                unique: true);
        }
    }
}
