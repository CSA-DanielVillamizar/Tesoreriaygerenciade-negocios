using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAMAMedellin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExpandirPerfilMiembro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Documento",
                table: "Miembros");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Miembros");

            migrationBuilder.RenameColumn(
                name: "TipoMiembro",
                table: "Miembros",
                newName: "Rango");

            migrationBuilder.RenameColumn(
                name: "TipoAfiliacion",
                table: "Miembros",
                newName: "TipoSangre");

            migrationBuilder.RenameColumn(
                name: "Telefono",
                table: "Miembros",
                newName: "TelefonoContactoEmergencia");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Miembros",
                newName: "ModeloMoto");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Miembros",
                newName: "Cilindraje");

            migrationBuilder.AlterColumn<string>(
                name: "Nombres",
                table: "Miembros",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FechaIngreso",
                table: "Miembros",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentoIdentidad",
                table: "Miembros",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Apodo",
                table: "Miembros",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Apellidos",
                table: "Miembros",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120);

            migrationBuilder.AddColumn<bool>(
                name: "EsActivo",
                table: "Miembros",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "MarcaMoto",
                table: "Miembros",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NombreContactoEmergencia",
                table: "Miembros",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Placa",
                table: "Miembros",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(@"
UPDATE m
SET DocumentoIdentidad = CONCAT('DOC-', RIGHT(REPLACE(CONVERT(varchar(36), m.Id), '-', ''), 12))
FROM Miembros m
WHERE m.DocumentoIdentidad IS NULL OR LTRIM(RTRIM(m.DocumentoIdentidad)) = '';

;WITH duplicados AS (
    SELECT
        Id,
        DocumentoIdentidad,
        ROW_NUMBER() OVER (PARTITION BY DocumentoIdentidad ORDER BY Id) AS rn
    FROM Miembros
)
UPDATE m
SET DocumentoIdentidad = LEFT(CONCAT(d.DocumentoIdentidad, '-', d.rn), 50)
FROM Miembros m
INNER JOIN duplicados d ON d.Id = m.Id
WHERE d.rn > 1;
");

            migrationBuilder.CreateIndex(
                name: "IX_Miembros_DocumentoIdentidad",
                table: "Miembros",
                column: "DocumentoIdentidad",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Miembros_DocumentoIdentidad",
                table: "Miembros");

            migrationBuilder.DropColumn(
                name: "EsActivo",
                table: "Miembros");

            migrationBuilder.DropColumn(
                name: "MarcaMoto",
                table: "Miembros");

            migrationBuilder.DropColumn(
                name: "NombreContactoEmergencia",
                table: "Miembros");

            migrationBuilder.DropColumn(
                name: "Placa",
                table: "Miembros");

            migrationBuilder.RenameColumn(
                name: "TipoSangre",
                table: "Miembros",
                newName: "TipoAfiliacion");

            migrationBuilder.RenameColumn(
                name: "TelefonoContactoEmergencia",
                table: "Miembros",
                newName: "Telefono");

            migrationBuilder.RenameColumn(
                name: "Rango",
                table: "Miembros",
                newName: "TipoMiembro");

            migrationBuilder.RenameColumn(
                name: "ModeloMoto",
                table: "Miembros",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "Cilindraje",
                table: "Miembros",
                newName: "Estado");

            migrationBuilder.AlterColumn<string>(
                name: "Nombres",
                table: "Miembros",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FechaIngreso",
                table: "Miembros",
                type: "date",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentoIdentidad",
                table: "Miembros",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Apodo",
                table: "Miembros",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Apellidos",
                table: "Miembros",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<string>(
                name: "Documento",
                table: "Miembros",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Miembros",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
