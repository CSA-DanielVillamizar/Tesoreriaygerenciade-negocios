using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class EgresosMultiMoneda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Agregar columna Moneda si no existe
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
                              WHERE TABLE_NAME='Egresos' AND COLUMN_NAME='Moneda')
                BEGIN
                    ALTER TABLE [Egresos] ADD [Moneda] int NOT NULL DEFAULT 1;
                END
            ");

            // Agregar columna TrmAplicada si no existe
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
                              WHERE TABLE_NAME='Egresos' AND COLUMN_NAME='TrmAplicada')
                BEGIN
                    ALTER TABLE [Egresos] ADD [TrmAplicada] decimal(18,4) NULL;
                END
            ");

            // Agregar columna ValorMonedaOriginal si no existe
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
                              WHERE TABLE_NAME='Egresos' AND COLUMN_NAME='ValorMonedaOriginal')
                BEGIN
                    ALTER TABLE [Egresos] ADD [ValorMonedaOriginal] decimal(18,2) NOT NULL DEFAULT 0;
                END
            ");

            // Migrar datos existentes: copiar ValorCop a ValorMonedaOriginal si está vacío
            migrationBuilder.Sql("UPDATE Egresos SET ValorMonedaOriginal = ValorCop WHERE ValorMonedaOriginal = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Moneda",
                table: "Egresos");

            migrationBuilder.DropColumn(
                name: "TrmAplicada",
                table: "Egresos");

            migrationBuilder.DropColumn(
                name: "ValorMonedaOriginal",
                table: "Egresos");
        }
    }
}
