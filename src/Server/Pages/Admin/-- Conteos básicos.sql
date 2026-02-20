-- Conteos básicos
SELECT DB_NAME() AS Db, COUNT(*) AS TotalTablas FROM sys.tables;

SELECT COUNT(*) AS Ingresos FROM dbo.Ingresos;
SELECT COUNT(*) AS Egresos FROM dbo.Egresos;
SELECT COUNT(*) AS Movimientos FROM dbo.MovimientosTesoreria;

-- Columnas de Egresos / Ingresos
SELECT 'Egresos' AS Tabla, name AS Columna, system_type_name AS Tipo
FROM sys.dm_exec_describe_first_result_set(N'SELECT * FROM dbo.Egresos', NULL, 0)
ORDER BY column_ordinal;

SELECT 'Ingresos' AS Tabla, name AS Columna, system_type_name AS Tipo
FROM sys.dm_exec_describe_first_result_set(N'SELECT * FROM dbo.Ingresos', NULL, 0)
ORDER BY column_ordinal;

-- Muestras sin ordenar por fecha para evitar errores de columna
SELECT TOP 5 * FROM dbo.Egresos ORDER BY (SELECT NULL);
SELECT TOP 5 * FROM dbo.Ingresos ORDER BY (SELECT NULL);


-- Limpiar seed-historico anterior (opcional)
DELETE FROM dbo.Egresos WHERE CreatedBy = 'seed-historico';

-- INSERT simplificado: Enero 2025 (18 Ingresos, 6 Egresos)
INSERT INTO dbo.Ingresos (Id, NumeroIngreso, FechaIngreso, Categoria, Descripcion, ValorCop, MetodoPago, ReferenciaTransaccion, Observaciones, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy, ImportRowHash)
VALUES 
(NEWID(), 'ING-2025-001', '2025-01-01', 'Cuota Ordinaria', 'Enero 2025 - Cuota', 150000, 'Transferencia', 'REF-001', NULL, GETDATE(), 'import-historico', GETDATE(), 'import-historico', NULL),
(NEWID(), 'ING-2025-002', '2025-01-15', 'Donación', 'Enero 2025 - Donación', 50000, 'Efectivo', NULL, NULL, GETDATE(), 'import-historico', GETDATE(), 'import-historico', NULL);
-- ... (agregar los 69 ingresos restantes)

INSERT INTO dbo.Egresos (Id, Fecha, Categoria, Proveedor, Descripcion, ValorCop, SoporteUrl, UsuarioRegistro, CreatedAt, CreatedBy, ImportRowHash)
VALUES
(NEWID(), '2025-01-05', 'Servicios', 'Proveedor', 'Enero 2025 - Servicio', 25000, NULL, 'import-historico', GETDATE(), 'import-historico', NULL),
(NEWID(), '2025-01-10', 'Suministros', 'Proveedor', 'Enero 2025 - Suministro', 35000, NULL, 'import-historico', GETDATE(), 'import-historico', NULL);
-- ... (agregar los 39 egresos restantes)