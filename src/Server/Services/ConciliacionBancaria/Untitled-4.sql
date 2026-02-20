-- ==============================================================================
-- SCRIPT DE LIMPIEZA: ELIMINAR TODOS LOS MOVIMIENTOS 2025
-- ==============================================================================
-- Propósito: Eliminar los 211 registros existentes de MovimientosTesoreria 2025
--            para permitir reimportación limpia desde Excel
-- Rango: MV-2025-00001 a MV-2025-00211 (Enero-Diciembre)
-- Base de datos: Azure SQL (sqldb-tesorerialamamedellin-prod)
-- ==============================================================================
-- INSTRUCCIONES:
-- 1. Revisar conteo ANTES de ejecutar
-- 2. Ejecutar DELETE dentro de transacción
-- 3. Verificar conteo DESPUÉS
-- 4. COMMIT si todo está correcto, ROLLBACK si hay error
-- ==============================================================================

-- Paso 1: VERIFICAR datos existentes ANTES de eliminar
PRINT '========================================';
PRINT 'PASO 1: VERIFICACIÓN DE DATOS EXISTENTES';
PRINT '========================================';
PRINT '';

SELECT 
    'ANTES DE ELIMINAR' AS Momento,
    COUNT(*) AS TotalRegistros,
    MIN(NumeroMovimiento) AS PrimerMovimiento,
    MAX(NumeroMovimiento) AS UltimoMovimiento,
    SUM(CASE WHEN Tipo = 1 THEN 1 ELSE 0 END) AS TotalIngresos,
    SUM(CASE WHEN Tipo = 2 THEN 1 ELSE 0 END) AS TotalEgresos
FROM MovimientosTesoreria
WHERE NumeroMovimiento LIKE 'MV-2025-%'
    AND NumeroMovimiento BETWEEN 'MV-2025-00001' AND 'MV-2025-00211';

PRINT '';
PRINT 'Desglose por mes:';
SELECT 
    ImportSheet AS HojaMes,
    COUNT(*) AS Registros,
    SUM(CASE WHEN Tipo = 1 THEN Valor ELSE 0 END) AS TotalIngresos,
    SUM(CASE WHEN Tipo = 2 THEN Valor ELSE 0 END) AS TotalEgresos
FROM MovimientosTesoreria
WHERE NumeroMovimiento LIKE 'MV-2025-%'
    AND NumeroMovimiento BETWEEN 'MV-2025-00001' AND 'MV-2025-00211'
GROUP BY ImportSheet
ORDER BY ImportSheet;

PRINT '';
PRINT '========================================';
PRINT 'PASO 2: ELIMINACIÓN DE DATOS';
PRINT '========================================';
PRINT '';

-- Paso 2: INICIAR TRANSACCIÓN para poder hacer rollback si es necesario
BEGIN TRANSACTION;

PRINT 'Transacción iniciada...';
PRINT '';

-- Paso 3: ELIMINAR todos los movimientos 2025
DELETE FROM MovimientosTesoreria
WHERE NumeroMovimiento LIKE 'MV-2025-%'
    AND NumeroMovimiento BETWEEN 'MV-2025-00001' AND 'MV-2025-00211';

PRINT CONCAT('Registros eliminados: ', @@ROWCOUNT);
PRINT '';

-- Paso 4: VERIFICAR que no queden registros 2025
PRINT '========================================';
PRINT 'PASO 3: VERIFICACIÓN DESPUÉS DE ELIMINAR';
PRINT '========================================';
PRINT '';

DECLARE @RegistrosRestantes INT;
SELECT @RegistrosRestantes = COUNT(*)
FROM MovimientosTesoreria
WHERE NumeroMovimiento LIKE 'MV-2025-%'
    AND NumeroMovimiento BETWEEN 'MV-2025-00001' AND 'MV-2025-00211';

PRINT CONCAT('Registros restantes en rango MV-2025-00001 a MV-2025-00211: ', @RegistrosRestantes);
PRINT '';

IF @RegistrosRestantes = 0
BEGIN
    PRINT '✓ ÉXITO: Todos los registros 2025 han sido eliminados correctamente.';
    PRINT '';
    PRINT '========================================';
    PRINT 'COMMIT DE TRANSACCIÓN';
    PRINT '========================================';
    PRINT '';
    PRINT '⚠ IMPORTANTE: Ejecuta COMMIT para confirmar la eliminación,';
    PRINT '   o ROLLBACK para deshacer los cambios.';
    PRINT '';
    PRINT '   Para confirmar eliminación, ejecuta: COMMIT TRANSACTION;';
    PRINT '   Para cancelar, ejecuta: ROLLBACK TRANSACTION;';
    PRINT '';
    -- COMMIT TRANSACTION; -- Descomenta esta línea para confirmar automáticamente
END
ELSE
BEGIN
    PRINT '✗ ERROR: Aún quedan registros después de eliminar.';
    PRINT '   Se recomienda ejecutar ROLLBACK y revisar.';
    PRINT '';
    -- ROLLBACK TRANSACTION; -- Descomenta para revertir automáticamente
END

-- ==============================================================================
-- SIGUIENTE PASO DESPUÉS DE COMMIT:
-- ==============================================================================
-- Una vez confirmada la eliminación (COMMIT), puedes ejecutar:
-- 1. IMPORT_ENERO_OCTUBRE_COMPLETO.sql (118 registros)
-- 2. IMPORT_NOV_DIC.sql (93 registros)
-- Total esperado después de importar: 211 registros
-- ==============================================================================
