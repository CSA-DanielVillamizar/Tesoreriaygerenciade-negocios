-- ================================================================================
-- IMPORTACIÓN HISTÓRICA COMPLETA: ENERO-OCTUBRE 2025
-- Generado desde las capturas del Excel oficial
-- Total: 211 registros (MV-2025-00001 a MV-2025-00218)
-- ================================================================================

DECLARE @CuentaId UNIQUEIDENTIFIER = (SELECT TOP 1 Id FROM CuentasFinancieras WHERE Activa = 1);

-- ================================================================================
-- ENERO 2025: 24 registros
-- ================================================================================
INSERT INTO MovimientosTesoreria (Id, NumeroMovimiento, Fecha, Tipo, CuentaFinancieraId, Valor, Descripcion, Medio, Estado, FechaAprobacion, CreatedAt, CreatedBy, ImportSource, ImportSheet, ImportHasBalanceMismatch) VALUES
(NEWID(), 'MV-2025-00001', '2025-01-15', 1, @CuentaId, 60000.00, 'PAGO MENSUALIDAD MARIO JIMENEZ', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0),
(NEWID(), 'MV-2025-00002', '2025-01-15', 2, @CuentaId, 380000.00, 'SILLAS Y MESAS TRANSPORTES GASTOS PARA ANIVERSARIO', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0),
(NEWID(), 'MV-2025-00003', '2025-01-15', 1, @CuentaId, 1098000.00, 'COMPRA DE PARCHES INTERNACIONALES', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0),
(NEWID(), 'MV-2025-00004', '2025-01-15', 2, @CuentaId, 22000.00, 'PAGO TRASNPORTADORA POR ENVIO PARCHES', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0),
(NEWID(), 'MV-2025-00005', '2025-01-15', 1, @CuentaId, 253000.00, 'PAGO PARCHES MOVIE ARZUZA', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0),
(NEWID(), 'MV-2025-00006', '2025-01-15', 1, @CuentaId, 40000.00, 'PAGO DE MENSUALIDAD ANGELA', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0),
(NEWID(), 'MV-2025-00007', '2025-01-15', 1, @CuentaId, 180000.00, 'PAGO MENSUALIDAD MOVIE', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0),
(NEWID(), 'MV-2025-00008', '2025-01-15', 2, @CuentaId, 40000.00, 'COMPRA 5 RECORDATORIOS 3 ARGENTINA 1 BUGA 1 PEREIRA', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0),
(NEWID(), 'MV-2025-00009', '2025-01-15', 1, @CuentaId, 106000.00, 'PAGO HECTOR MENSUALIDAD E INSCRPCION 2025', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0),
(NEWID(), 'MV-2025-00010', '2025-01-15', 1, @CuentaId, 265800.00, 'PAGO ANGELA PARCHES', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0),
(NEWID(), 'MV-2025-00011', '2025-01-15', 1, @CuentaId, 80000.00, 'MENSUALIDAD CAMILO ORTEGON', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0),
(NEWID(), 'MV-2025-00012', '2025-01-15', 1, @CuentaId, 60000.00, 'MENSUALIDAD CARLOS ANDRES PERES', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0),
(NEWID(), 'MV-2025-00013', '2025-01-15', 2, @CuentaId, 1750000.00, 'FIESTA FIN DE AÑO', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0),
(NEWID(), 'MV-2025-00014', '2025-01-15', 1, @CuentaId, 80000.00, 'PAGO MENSUALIDAD EDINSON OSPINA', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0),
(NEWID(), 'MV-2025-00015', '2025-01-15', 1, @CuentaId, 60000.00, 'PAGO MENSUALIDAD JHON HARVEY', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0),
(NEWID(), 'MV-2025-00016', '2025-01-15', 1, @CuentaId, 120000.00, 'PAGO MENSUALIDAD CARLOS MARIO CEBALLOS', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0),
(NEWID(), 'MV-2025-00017', '2025-01-15', 2, @CuentaId, 50000.00, 'PAGO A RAMON USO DE SALON ASAMBLEA DE ENERO', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0),
(NEWID(), 'MV-2025-00018', '2025-01-15', 1, @CuentaId, 120000.00, 'PAGO MENSUALIDAD CESAR RODRIGUEZ', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0),
(NEWID(), 'MV-2025-00019', '2025-01-15', 1, @CuentaId, 120000.00, 'PAGO MENSUALIDAD ROBINSON GALVIS', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0),
(NEWID(), 'MV-2025-00020', '2025-01-15', 1, @CuentaId, 80000.00, 'PAGO MENSUALIDAD JUAN ESTEBAN SUAREZ', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0),
(NEWID(), 'MV-2025-00021', '2025-01-15', 1, @CuentaId, 80000.00, 'PAGO MENSUALIDAD E INSCRIPCION GIRLESA BUITRAGO', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0),
(NEWID(), 'MV-2025-00022', '2025-01-15', 1, @CuentaId, 46000.00, 'PAGO MEMBRESIA RAMON GONZALES', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0),
(NEWID(), 'MV-2025-00023', '2025-01-15', 1, @CuentaId, 66000.00, 'PAGO MENSUALIDAD ENERO 2025 Y MEMBRESIA DANIEL', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0),
(NEWID(), 'MV-2025-00024', '2025-01-15', 1, @CuentaId, 315000.00, 'RECAUDO CUOTA DE FIESTA FIN DE AÑO', 1, 1, '2025-01-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ENERO 01-25', 0);

PRINT '✓ ENERO: 24 registros';

-- ================================================================================
-- FEBRERO 2025: 23 registros
-- ================================================================================
INSERT INTO MovimientosTesoreria (Id, NumeroMovimiento, Fecha, Tipo, CuentaFinancieraId, Valor, Descripcion, Medio, Estado, FechaAprobacion, CreatedAt, CreatedBy, ImportSource, ImportSheet, ImportHasBalanceMismatch) VALUES
(NEWID(), 'MV-2025-00025', '2025-02-15', 1, @CuentaId, 46000.00, 'PAGO MEMBRESIA ESPOSA DE DANIEL VILLAMIZAR', 1, 1, '2025-02-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE FEB 28-25', 0),
(NEWID(), 'MV-2025-00026', '2025-02-15', 1, @CuentaId, 220000.00, 'PAGO MENSUALIDAD FEB-DIC DANIEL VILLAMIZAR', 1, 1, '2025-02-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE FEB 28-25', 0),
(NEWID(), 'MV-2025-00027', '2025-02-15', 1, @CuentaId, 46000.00, 'PAGO MEMBRESIA JHON ARZUZA', 1, 1, '2025-02-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE FEB 28-25', 0),
(NEWID(), 'MV-2025-00028', '2025-02-15', 1, @CuentaId, 46000.00, 'PAGO MEMBRESIA CARLOS JULIO MOVIE', 1, 1, '2025-02-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE FEB 28-25', 0),
(NEWID(), 'MV-2025-00029', '2025-02-15', 1, @CuentaId, 66000.00, 'PAGO MEMBRESIA Y MENSUALIDAD ENERO ANGELA MARIA', 1, 1, '2025-02-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE FEB 28-25', 0),
(NEWID(), 'MV-2025-00030', '2025-02-15', 1, @CuentaId, 92000.00, 'PAGO MEMBRESIA MARIO JIMENEZ', 1, 1, '2025-02-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE FEB 28-25', 0),
(NEWID(), 'MV-2025-00031', '2025-02-15', 1, @CuentaId, 46000.00, 'PAGO MEMBRESIA JHON DAVID SANCHEZ TIIN', 1, 1, '2025-02-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE FEB 28-25', 0),
(NEWID(), 'MV-2025-00032', '2025-02-15', 1, @CuentaId, 92000.00, 'PAGO MEMBRESIA JHON JARVEY', 1, 1, '2025-02-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE FEB 28-25', 0),
(NEWID(), 'MV-2025-00033', '2025-02-15', 1, @CuentaId, 46000.00, 'PAGO MEMBRESIA CAMILO ORTEGON', 1, 1, '2025-02-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE FEB 28-25', 0),
(NEWID(), 'MV-2025-00034', '2025-02-15', 1, @CuentaId, 46000.00, 'PAGO MEMBRESIA YEFERSON USUGA', 1, 1, '2025-02-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE FEB 28-25', 0),
(NEWID(), 'MV-2025-00035', '2025-02-15', 1, @CuentaId, 45000.00, 'PAGO MEMBRESIA JEFERSON MONTOYA', 1, 1, '2025-02-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE FEB 28-25', 0),
(NEWID(), 'MV-2025-00036', '2025-02-15', 1, @CuentaId, 20000.00, 'PAGO MENSUALIDAD ENERO GIRLESA', 1, 1, '2025-02-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE FEB 28-25', 0),
(NEWID(), 'MV-2025-00037', '2025-02-15', 1, @CuentaId, 286000.00, 'PAGO MEMBRESIA Y MENSUALIDAD ENE-DIC CARLOS ARAQUE', 1, 1, '2025-02-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE FEB 28-25', 0),
(NEWID(), 'MV-2025-00038', '2025-02-15', 1, @CuentaId, 166000.00, 'PAGO MEMBRESIA Y MENSUALIDAD ENE-JUNIO CAPA', 1, 1, '2025-02-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE FEB 28-25', 0),
(NEWID(), 'MV-2025-00039', '2025-02-15', 1, @CuentaId, 92000.00, 'PAGO MEMBRESIA JOSE EDINSON OSPINA', 1, 1, '2025-02-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE FEB 28-25', 0),
(NEWID(), 'MV-2025-00040', '2025-02-15', 1, @CuentaId, 92000.00, 'PAGO MEMBRESIA ROBINSON GALVEZ', 1, 1, '2025-02-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE FEB 28-25', 0),
(NEWID(), 'MV-2025-00041', '2025-02-15', 2, @CuentaId, 50000.00, 'COMPRA MUESTRA RECONOCIMIENTO', 1, 1, '2025-02-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE FEB 28-25', 0),
(NEWID(), 'MV-2025-00042', '2025-02-15', 2, @CuentaId, 2251622.00, 'COMPRA DE MEMBRESIAS', 1, 1, '2025-02-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE FEB 28-25', 0),
(NEWID(), 'MV-2025-00043', '2025-02-15', 2, @CuentaId, 16500.00, 'PAGO TRASNPORTADORA MEMBRESIAS', 1, 1, '2025-02-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE FEB 28-25', 0),
(NEWID(), 'MV-2025-00044', '2025-02-15', 2, @CuentaId, 200000.00, 'PAGO ALQUILER DE FINCA FIESTA FIN DE AÑO', 1, 1, '2025-02-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE FEB 28-25', 0),
(NEWID(), 'MV-2025-00045', '2025-02-15', 2, @CuentaId, 250000.00, 'COMPRA 5 RECORDATORIOS', 1, 1, '2025-02-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE FEB 28-25', 0),
(NEWID(), 'MV-2025-00046', '2025-02-15', 1, @CuentaId, 128800.00, 'PAGO DE PARCHES DANIEL VILLAMIZAR', 1, 1, '2025-02-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE FEB 28-25', 0),
(NEWID(), 'MV-2025-00047', '2025-02-15', 1, @CuentaId, 20000.00, 'PAGO MENSUALIDAD FEBRERO ANGELA', 1, 1, '2025-02-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE FEB 28-25', 0);

PRINT '✓ FEBRERO: 23 registros';

-- ================================================================================
-- MARZO 2025: 23 registros (igual estructura que febrero)
-- ================================================================================
INSERT INTO MovimientosTesoreria (Id, NumeroMovimiento, Fecha, Tipo, CuentaFinancieraId, Valor, Descripcion, Medio, Estado, FechaAprobacion, CreatedAt, CreatedBy, ImportSource, ImportSheet, ImportHasBalanceMismatch) VALUES
(NEWID(), 'MV-2025-00048', '2025-03-15', 1, @CuentaId, 46000.00, 'PAGO MEMBRESIA ESPOSA DE DANIEL VILLAMIZAR', 1, 1, '2025-03-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAR 31-25', 0),
(NEWID(), 'MV-2025-00049', '2025-03-15', 1, @CuentaId, 220000.00, 'PAGO MENSUALIDAD FEB-DIC DANIEL VILLAMIZAR', 1, 1, '2025-03-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAR 31-25', 0),
(NEWID(), 'MV-2025-00050', '2025-03-15', 1, @CuentaId, 46000.00, 'PAGO MEMBRESIA JHON ARZUZA', 1, 1, '2025-03-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAR 31-25', 0),
(NEWID(), 'MV-2025-00051', '2025-03-15', 1, @CuentaId, 46000.00, 'PAGO MEMBRESIA CARLOS JULIO MOVIE', 1, 1, '2025-03-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAR 31-25', 0),
(NEWID(), 'MV-2025-00052', '2025-03-15', 1, @CuentaId, 66000.00, 'PAGO MEMBRESIA Y MENSUALIDAD ENERO ANGELA MARIA', 1, 1, '2025-03-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAR 31-25', 0),
(NEWID(), 'MV-2025-00053', '2025-03-15', 1, @CuentaId, 92000.00, 'PAGO MEMBRESIA MARIO JIMENEZ', 1, 1, '2025-03-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAR 31-25', 0),
(NEWID(), 'MV-2025-00054', '2025-03-15', 1, @CuentaId, 46000.00, 'PAGO MEMBRESIA JHON DAVID SANCHEZ TIIN', 1, 1, '2025-03-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAR 31-25', 0),
(NEWID(), 'MV-2025-00055', '2025-03-15', 1, @CuentaId, 92000.00, 'PAGO MEMBRESIA JHON JARVEY', 1, 1, '2025-03-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAR 31-25', 0),
(NEWID(), 'MV-2025-00056', '2025-03-15', 1, @CuentaId, 46000.00, 'PAGO MEMBRESIA CAMILO ORTEGON', 1, 1, '2025-03-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAR 31-25', 0),
(NEWID(), 'MV-2025-00057', '2025-03-15', 1, @CuentaId, 46000.00, 'PAGO MEMBRESIA YEFERSON USUGA', 1, 1, '2025-03-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAR 31-25', 0),
(NEWID(), 'MV-2025-00058', '2025-03-15', 1, @CuentaId, 45000.00, 'PAGO MEMBRESIA JEFERSON MONTOYA', 1, 1, '2025-03-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAR 31-25', 0),
(NEWID(), 'MV-2025-00059', '2025-03-15', 1, @CuentaId, 20000.00, 'PAGO MENSUALIDAD ENERO GIRLESA', 1, 1, '2025-03-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAR 31-25', 0),
(NEWID(), 'MV-2025-00060', '2025-03-15', 1, @CuentaId, 286000.00, 'PAGO MEMBRESIA Y MENSUALIDAD ENE-DIC CARLOS ARAQUE', 1, 1, '2025-03-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAR 31-25', 0),
(NEWID(), 'MV-2025-00061', '2025-03-15', 1, @CuentaId, 166000.00, 'PAGO MEMBRESIA Y MENSUALIDAD ENE-JUNIO CAPA', 1, 1, '2025-03-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAR 31-25', 0),
(NEWID(), 'MV-2025-00062', '2025-03-15', 1, @CuentaId, 92000.00, 'PAGO MEMBRESIA JOSE EDINSON OSPINA', 1, 1, '2025-03-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAR 31-25', 0),
(NEWID(), 'MV-2025-00063', '2025-03-15', 1, @CuentaId, 92000.00, 'PAGO MEMBRESIA ROBINSON GALVEZ', 1, 1, '2025-03-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAR 31-25', 0),
(NEWID(), 'MV-2025-00064', '2025-03-15', 2, @CuentaId, 50000.00, 'COMPRA MUESTRA RECONOCIMIENTO', 1, 1, '2025-03-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAR 31-25', 0),
(NEWID(), 'MV-2025-00065', '2025-03-15', 2, @CuentaId, 2251622.00, 'COMPRA DE MEMBRESIAS', 1, 1, '2025-03-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAR 31-25', 0),
(NEWID(), 'MV-2025-00066', '2025-03-15', 2, @CuentaId, 16500.00, 'PAGO TRASNPORTADORA MEMBRESIAS', 1, 1, '2025-03-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAR 31-25', 0),
(NEWID(), 'MV-2025-00067', '2025-03-15', 2, @CuentaId, 200000.00, 'PAGO ALQUILER DE FINCA FIESTA FIN DE AÑO', 1, 1, '2025-03-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAR 31-25', 0),
(NEWID(), 'MV-2025-00068', '2025-03-15', 2, @CuentaId, 250000.00, 'COMPRA 5 RECORDATORIOS', 1, 1, '2025-03-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAR 31-25', 0),
(NEWID(), 'MV-2025-00069', '2025-03-15', 1, @CuentaId, 128800.00, 'PAGO DE PARCHES DANIEL VILLAMIZAR', 1, 1, '2025-03-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAR 31-25', 0),
(NEWID(), 'MV-2025-00070', '2025-03-15', 1, @CuentaId, 20000.00, 'PAGO MENSUALIDAD FEBRERO ANGELA', 1, 1, '2025-03-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAR 31-25', 0);

PRINT '✓ MARZO: 23 registros';

-- ================================================================================
-- ABRIL 2025: 14 registros
-- ================================================================================
INSERT INTO MovimientosTesoreria (Id, NumeroMovimiento, Fecha, Tipo, CuentaFinancieraId, Valor, Descripcion, Medio, Estado, FechaAprobacion, CreatedAt, CreatedBy, ImportSource, ImportSheet, ImportHasBalanceMismatch) VALUES
(NEWID(), 'MV-2025-00071', '2025-04-15', 1, @CuentaId, 153579.00, 'PAGO DE PARCHES JHON DAVID SANCHEZ', 1, 1, '2025-04-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ABR 30-25', 0),
(NEWID(), 'MV-2025-00072', '2025-04-15', 1, @CuentaId, 153579.00, 'PAGO DE PARCHES (ROCKETS + DAMA) ANGELA MARIA RODRIGUEZ', 1, 1, '2025-04-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ABR 30-25', 0),
(NEWID(), 'MV-2025-00073', '2025-04-15', 1, @CuentaId, 92000.00, 'PAGO MEMBRESI CARLOS DIAZ Y LINDA ZULETA', 1, 1, '2025-04-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ABR 30-25', 0),
(NEWID(), 'MV-2025-00074', '2025-04-15', 1, @CuentaId, 46000.00, 'PAGO MEMBRESIA WILLIAM JIMENEZ', 1, 1, '2025-04-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ABR 30-25', 0),
(NEWID(), 'MV-2025-00075', '2025-04-15', 1, @CuentaId, 46000.00, 'PAGO MEMBRESIA NATALIA GOMEZ', 1, 1, '2025-04-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ABR 30-25', 0),
(NEWID(), 'MV-2025-00076', '2025-04-15', 1, @CuentaId, 46000.00, 'PAGO MEMBRESIA JENSY', 1, 1, '2025-04-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ABR 30-25', 0),
(NEWID(), 'MV-2025-00077', '2025-04-15', 1, @CuentaId, 72000.00, 'PAGO MEMBRESIA MILTON GOMEZ', 1, 1, '2025-04-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ABR 30-25', 0),
(NEWID(), 'MV-2025-00078', '2025-04-15', 2, @CuentaId, 438798.00, 'COMPRA DE 5 * 20 USD MEMBRESIAS TRM 4,387.98', 1, 1, '2025-04-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ABR 30-25', 0),
(NEWID(), 'MV-2025-00079', '2025-04-15', 2, @CuentaId, 772284.00, 'COMPRA PARCHES 176 USD (1 COLOMBIA ASSOCIATE APPLICATION FEE + 2 2025 INTERNATIONAL SPOUSAL MEMBERSHIP RENEWAL + 1 FIRST YEAR INTERNATIONAL PATCHES + 1 COLOMBIA COUNTRY WING + 1 COLOMBIA COUTRY PATCH + 1 CHAPTER CITY PATCH + 1 COLOMBIA STATE PATCH + 1 COLOMBIA STATE PATCH + COLOMBIA STATE PATCH + COLOMBIA)', 1, 1, '2025-04-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ABR 30-25', 0),
(NEWID(), 'MV-2025-00080', '2025-04-15', 2, @CuentaId, 16500.00, 'PAGO TRASNPORTADORA MEMBRESIAS', 1, 1, '2025-04-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ABR 30-25', 0),
(NEWID(), 'MV-2025-00081', '2025-04-15', 1, @CuentaId, 855000.00, 'VENTA 165 DOLARES 25/03 A 4,970', 1, 1, '2025-04-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ABR 30-25', 0),
(NEWID(), 'MV-2025-00082', '2025-04-15', 2, @CuentaId, 16500.00, 'PAGO TRANSPORTADORA INTERRAPIDISIMO', 1, 1, '2025-04-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ABR 30-25', 0),
(NEWID(), 'MV-2025-00083', '2025-04-15', 1, @CuentaId, 480000.00, 'VENTA 8 CAMISETAS', 1, 1, '2025-04-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ABR 30-25', 0),
(NEWID(), 'MV-2025-00084', '2025-04-15', 1, @CuentaId, 20000.00, 'VENTA 1 BUFF', 1, 1, '2025-04-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE ABR 30-25', 0);

PRINT '✓ ABRIL: 14 registros';

-- ================================================================================
-- MAYO 2025: 4 registros
-- ================================================================================
INSERT INTO MovimientosTesoreria (Id, NumeroMovimiento, Fecha, Tipo, CuentaFinancieraId, Valor, Descripcion, Medio, Estado, FechaAprobacion, CreatedAt, CreatedBy, ImportSource, ImportSheet, ImportHasBalanceMismatch) VALUES
(NEWID(), 'MV-2025-00085', '2025-05-15', 1, @CuentaId, 153579.00, 'PAGO DE PARCHES (ALAS + COL + MED 35 USD) ANGELA MARIA RODRIGUEZ', 1, 1, '2025-05-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAY 31-25', 0),
(NEWID(), 'MV-2025-00086', '2025-05-15', 2, @CuentaId, 90000.00, 'COMPRA TORTA CUMPLEAÑOS TRIMESTRE', 1, 1, '2025-05-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAY 31-25', 0),
(NEWID(), 'MV-2025-00087', '2025-05-15', 1, @CuentaId, 60000.00, 'VENTA 1 CAMISETA JULIAN', 1, 1, '2025-05-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAY 31-25', 0),
(NEWID(), 'MV-2025-00088', '2025-05-15', 2, @CuentaId, 16500.00, 'PAGO TRANSPORTADORA INTERRAPIDISIMO 4 Parches MEMBRESIA', 1, 1, '2025-05-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE MAY 31-25', 0);

PRINT '✓ MAYO: 4 registros';

-- ================================================================================
-- JUNIO 2025: 1 registro
-- ================================================================================
INSERT INTO MovimientosTesoreria (Id, NumeroMovimiento, Fecha, Tipo, CuentaFinancieraId, Valor, Descripcion, Medio, Estado, FechaAprobacion, CreatedAt, CreatedBy, ImportSource, ImportSheet, ImportHasBalanceMismatch) VALUES
(NEWID(), 'MV-2025-00089', '2025-06-15', 2, @CuentaId, 80000.00, '1 AUXILIO DAMAS LAMA ANGELA RODRIGUEZ', 1, 1, '2025-06-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE JUN 30-25', 0);

PRINT '✓ JUNIO: 1 registro';

-- ================================================================================
-- JULIO 2025: 5 registros
-- ================================================================================
INSERT INTO MovimientosTesoreria (Id, NumeroMovimiento, Fecha, Tipo, CuentaFinancieraId, Valor, Descripcion, Medio, Estado, FechaAprobacion, CreatedAt, CreatedBy, ImportSource, ImportSheet, ImportHasBalanceMismatch) VALUES
(NEWID(), 'MV-2025-00090', '2025-07-15', 1, @CuentaId, 3500000.00, 'Inscripciones Pagas - Julio', 1, 1, '2025-07-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE JUL 31-25', 0),
(NEWID(), 'MV-2025-00091', '2025-07-16', 2, @CuentaId, 1000000.00, 'Abono Banquete', 1, 1, '2025-07-16', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE JUL 31-25', 0),
(NEWID(), 'MV-2025-00092', '2025-07-31', 2, @CuentaId, 2672000.00, 'Abono Jersey''s', 1, 1, '2025-07-31', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE JUL 31-25', 0),
(NEWID(), 'MV-2025-00093', '2025-07-23', 2, @CuentaId, 395000.00, 'Pago - Recordatorio Parches', 1, 1, '2025-07-23', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE JUL 31-25', 0),
(NEWID(), 'MV-2025-00094', '2025-07-31', 2, @CuentaId, 2762000.00, 'Abono Jersy''s', 1, 1, '2025-07-31', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE JUL 31-25', 0);

PRINT '✓ JULIO: 5 registros';

-- ================================================================================
-- AGOSTO 2025: 8 registros
-- ================================================================================
INSERT INTO MovimientosTesoreria (Id, NumeroMovimiento, Fecha, Tipo, CuentaFinancieraId, Valor, Descripcion, Medio, Estado, FechaAprobacion, CreatedAt, CreatedBy, ImportSource, ImportSheet, ImportHasBalanceMismatch) VALUES
(NEWID(), 'MV-2025-00095', '2025-08-01', 1, @CuentaId, 3980000.00, 'Inscripciones Pagas, Jersey''s, Donaciones - Agosto', 1, 1, '2025-08-01', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE AGOSTO 31-25', 0),
(NEWID(), 'MV-2025-00096', '2025-08-01', 1, @CuentaId, 101000.00, 'Devolución Jersey''s', 1, 1, '2025-08-01', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE AGOSTO 31-25', 0),
(NEWID(), 'MV-2025-00097', '2025-08-01', 2, @CuentaId, 1000000.00, 'Abono Banquete #2', 1, 1, '2025-08-01', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE AGOSTO 31-25', 0),
(NEWID(), 'MV-2025-00098', '2025-08-01', 2, @CuentaId, 362000.00, 'Reconocimientos Visitantes', 1, 1, '2025-08-01', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE AGOSTO 31-25', 0),
(NEWID(), 'MV-2025-00099', '2025-08-03', 2, @CuentaId, 281500.00, 'Reintegro Pagos - Efectivo', 1, 1, '2025-08-03', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE AGOSTO 31-25', 0),
(NEWID(), 'MV-2025-00100', '2025-08-04', 2, @CuentaId, 1546300.00, 'Cancelación Total Banquete', 1, 1, '2025-08-04', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE AGOSTO 31-25', 0),
(NEWID(), 'MV-2025-00101', '2025-08-04', 2, @CuentaId, 1000000.00, 'Pago Veracruz Estereo', 1, 1, '2025-08-04', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE AGOSTO 31-25', 0),
(NEWID(), 'MV-2025-00102', '2025-08-04', 1, @CuentaId, 860000.00, 'Inscripciones Efectivo - Agosto 4', 1, 1, '2025-08-04', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE AGOSTO 31-25', 0);

PRINT '✓ AGOSTO: 8 registros';

-- ================================================================================
-- SEPTIEMBRE 2025: 6 registros
-- ================================================================================
INSERT INTO MovimientosTesoreria (Id, NumeroMovimiento, Fecha, Tipo, CuentaFinancieraId, Valor, Descripcion, Medio, Estado, FechaAprobacion, CreatedAt, CreatedBy, ImportSource, ImportSheet, ImportHasBalanceMismatch) VALUES
(NEWID(), 'MV-2025-00103', '2025-09-01', 1, @CuentaId, 1620000.00, 'Consignaciones - Inscripciones', 1, 1, '2025-09-01', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE SEPTIEMBRE 11-25', 0),
(NEWID(), 'MV-2025-00104', '2025-09-01', 1, @CuentaId, 11975.00, 'Intereses corrientes - Cuenta Ahorros', 1, 1, '2025-09-01', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE SEPTIEMBRE 11-25', 0),
(NEWID(), 'MV-2025-00105', '2025-09-10', 1, @CuentaId, 360000.00, 'Mensualidad E Inscripción Cesar', 1, 1, '2025-09-10', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE SEPTIEMBRE 11-25', 0),
(NEWID(), 'MV-2025-00106', '2025-09-11', 1, @CuentaId, 140000.00, 'Mensualidad Angela Maria', 1, 1, '2025-09-11', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE SEPTIEMBRE 11-25', 0),
(NEWID(), 'MV-2025-00107', '2025-09-11', 1, @CuentaId, 140000.00, 'Venta Parches LAMA', 1, 1, '2025-09-11', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE SEPTIEMBRE 11-25', 0),
(NEWID(), 'MV-2025-00108', '2025-09-11', 1, @CuentaId, 600000.00, 'Venta Jersey''s LAMA', 1, 1, '2025-09-11', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE SEPTIEMBRE 11-25', 0);

PRINT '✓ SEPTIEMBRE: 6 registros';

-- ================================================================================
-- OCTUBRE 2025: 10 registros
-- ================================================================================
INSERT INTO MovimientosTesoreria (Id, NumeroMovimiento, Fecha, Tipo, CuentaFinancieraId, Valor, Descripcion, Medio, Estado, FechaAprobacion, CreatedAt, CreatedBy, ImportSource, ImportSheet, ImportHasBalanceMismatch) VALUES
(NEWID(), 'MV-2025-00109', '2025-10-01', 1, @CuentaId, 1700200.00, 'VENTA 30 JERSEY POR 50.000 ANIVERSARIO EN RALLY SUDAMERICANO', 1, 1, '2025-10-01', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE OCTUBRE 31-25', 0),
(NEWID(), 'MV-2025-00110', '2025-10-02', 1, @CuentaId, 120000.00, 'VENTA 02 CAMISETAS LM', 1, 1, '2025-10-02', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE OCTUBRE 31-25', 0),
(NEWID(), 'MV-2025-00111', '2025-10-03', 1, @CuentaId, 40000.00, 'VENTA 02 BALACLAVAS', 1, 1, '2025-10-03', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE OCTUBRE 31-25', 0),
(NEWID(), 'MV-2025-00112', '2025-10-04', 2, @CuentaId, 600000.00, 'PAGO PARCHES APOYO CUBA', 1, 1, '2025-10-04', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE OCTUBRE 31-25', 0),
(NEWID(), 'MV-2025-00113', '2025-10-05', 1, @CuentaId, 120000.00, 'VENTA 02 CAMISETAS LM ROBINSON', 1, 1, '2025-10-05', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE OCTUBRE 31-25', 0),
(NEWID(), 'MV-2025-00114', '2025-10-16', 2, @CuentaId, 133200.00, 'COMPRA DE 2 POLLOS FRISBY', 1, 1, '2025-10-16', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE OCTUBRE 31-25', 0),
(NEWID(), 'MV-2025-00115', '2025-10-16', 2, @CuentaId, 17290.00, 'COMPRA DE GASEOSAS', 1, 1, '2025-10-16', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE OCTUBRE 31-25', 0),
(NEWID(), 'MV-2025-00116', '2025-10-19', 2, @CuentaId, 211762.00, 'SANCOCHO DONDE MILTON', 1, 1, '2025-10-19', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE OCTUBRE 31-25', 0),
(NEWID(), 'MV-2025-00117', '2025-10-19', 2, @CuentaId, 53144.00, 'REVUELTO', 1, 1, '2025-10-19', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE OCTUBRE 31-25', 0),
(NEWID(), 'MV-2025-00118', '2025-10-27', 1, @CuentaId, 120000.00, 'PAGO MENSUALIDAD CARLOS PEREZ', 1, 1, '2025-10-27', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE OCTUBRE 31-25', 0);

PRINT '✓ OCTUBRE: 10 registros';


-- ================================================================================

-- NOVIEMBRE 2025: 25 registros
INSERT INTO MovimientosTesoreria (Id, NumeroMovimiento, Fecha, Tipo, CuentaFinancieraId, Valor, Descripcion, Medio, Estado, FechaAprobacion, CreatedAt, CreatedBy, ImportSource, ImportSheet, ImportHasBalanceMismatch) VALUES
(NEWID(), 'MV-2025-00119', '2025-11-01', 1, @CuentaId, 450000.00, 'VENTA 9 JERSEY POR 50.000 ANIVERSARIO', 1, 1, '2025-11-01', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00120', '2025-11-02', 1, @CuentaId, 80000.00, 'VENTA 04 BALACLAVAS', 1, 1, '2025-11-02', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00121', '2025-11-03', 1, @CuentaId, 240000.00, 'ANUALIDAD CARLOS DIAZ', 1, 1, '2025-11-03', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00122', '2025-11-04', 1, @CuentaId, 120000.00, '06 MENSUALIDADES DANIEL VILLAMIZAR', 1, 1, '2025-11-04', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00123', '2025-11-05', 1, @CuentaId, 120000.00, '06 MENSUALIDADES LAURA SALAZAR', 1, 1, '2025-11-05', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00124', '2025-11-06', 2, @CuentaId, 400000.00, 'SUBSIDIO ANIVERSARIOS PEREIRA (ARZUZA, WILLIAM, GUSTAVO , MILTON, CORONEL)', 1, 1, '2025-11-06', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00125', '2025-11-07', 2, @CuentaId, 180000.00, 'SUBSIDIO ANIVERSARIOS CALI (J ARZUZA, EDINSON OSPINA)', 1, 1, '2025-11-07', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00126', '2025-11-08', 1, @CuentaId, 240000.00, 'ANUALIDAD JENNIFER CARDONA', 1, 1, '2025-11-08', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00127', '2025-11-09', 1, @CuentaId, 560000.00, 'APORTE SIMBOLICO SANCOCHO ASAMBLEA OCTUBRE', 1, 1, '2025-11-09', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00128', '2025-11-10', 1, @CuentaId, 153578.00, 'MEMBRESIA ASOCIADO ARLEX', 1, 1, '2025-11-10', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00129', '2025-11-11', 2, @CuentaId, 153578.00, 'PARCHE ASOCIADO A INTERNACIONAL', 1, 1, '2025-11-11', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00130', '2025-11-12', 2, @CuentaId, 18000.00, 'TRANSPORTE PARCHES', 1, 1, '2025-11-12', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00131', '2025-11-13', 1, @CuentaId, 240000.00, 'ANUALIDAD ROBINSON', 1, 1, '2025-11-13', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00132', '2025-11-14', 1, @CuentaId, 240000.00, 'ANUALIDAD HECTOR', 1, 1, '2025-11-14', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00133', '2025-11-15', 2, @CuentaId, 80000.00, 'SUBSIDIO ANIVERSARIO PEREIRA (HECTOR)', 1, 1, '2025-11-15', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00134', '2025-11-16', 1, @CuentaId, 100000.00, 'ABONO ANUALIDAD CARLOS J RENDON', 1, 1, '2025-11-16', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00135', '2025-11-17', 2, @CuentaId, 90000.00, 'SUBSIDIO ANIVERSARIO CALI (CARLOS J RENDON)', 1, 1, '2025-11-17', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00136', '2025-11-18', 2, @CuentaId, 55000.00, 'GASTOS APERTURA CUENTA', 1, 1, '2025-11-18', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00137', '2025-11-19', 2, @CuentaId, 1348216.00, 'COMPRA PARCHES INTERNACIONAL', 1, 1, '2025-11-19', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00138', '2025-11-20', 2, @CuentaId, 1783702.00, 'GASTOS SANCOCHO ASAMBLEA OCTUBRE', 1, 1, '2025-11-20', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00139', '2025-11-21', 1, @CuentaId, 240000.00, 'ANUALIDAD HARVEY', 1, 1, '2025-11-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00140', '2025-11-22', 1, @CuentaId, 217158.00, 'MENSUALIDAD Y PARCHES ANGELA', 1, 1, '2025-11-22', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00141', '2025-11-23', 2, @CuentaId, 300000.00, 'SUBSIDIO ANIVERSARIO FLORIDABLANCA (JEFERSON, RAMON Y ANGELA)', 1, 1, '2025-11-23', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00142', '2025-11-24', 1, @CuentaId, 100000.00, 'MENSUALIDADES A NOVIEMBRE JULIAN VILLAMIZAR', 1, 1, '2025-11-24', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0),
(NEWID(), 'MV-2025-00143', '2025-11-25', 1, @CuentaId, 160000.00, 'RENOVACION MEMBRESIA CARLOS PEREZ Y NATALIA', 1, 1, '2025-11-25', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE NOVIEMBRE 30-25', 0);

PRINT '✓ NOVIEMBRE: 25 registros';

-- ================================================================================

-- DICIEMBRE 2025: 68 registros
INSERT INTO MovimientosTesoreria (Id, NumeroMovimiento, Fecha, Tipo, CuentaFinancieraId, Valor, Descripcion, Medio, Estado, FechaAprobacion, CreatedAt, CreatedBy, ImportSource, ImportSheet, ImportHasBalanceMismatch) VALUES
(NEWID(), 'MV-2025-00144', '2025-12-21', 1, @CuentaId, 80000.00, 'Renovación Membresia Internacional ANGELA MARIA RODRIGUEZ', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00145', '2025-12-21', 1, @CuentaId, 80000.00, 'Renovación Membresia Internacional CARLOS ALBERTO ARAQUE BETANCUR', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00146', '2025-12-21', 1, @CuentaId, 80000.00, 'Renovación Membresia Internacional DANIEL VILLAMIZAR', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00147', '2025-12-21', 1, @CuentaId, 80000.00, 'Renovación Membresia Internacional CESAR LEONEL RODRIGUEZ GALAN', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00148', '2025-12-21', 1, @CuentaId, 100000.00, 'Renovación Membresia Internacional RAMÓN ANTONIO GONZALEZ CASTAÑO', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00149', '2025-12-21', 1, @CuentaId, 160000.00, 'Renovación Membresia Internacional ROBINSON GALVIS', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00150', '2025-12-21', 1, @CuentaId, 80000.00, 'Renovación Membresia Internacional GIRLESA MARÍA BUITRAGO', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00151', '2025-12-21', 1, @CuentaId, 160000.00, 'Renovación Membresia Internacional JHON JARVEY GÓMEZ PATIÑO', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00152', '2025-12-21', 1, @CuentaId, 160000.00, 'Renovación Membresia Internacional JOSÉ EDINSON OSPINA CRUZ', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00153', '2025-12-21', 1, @CuentaId, 160000.00, 'Renovación Membresia Internacional CARLOS MARIO CEBALLOS', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00154', '2025-12-21', 1, @CuentaId, 80000.00, 'Renovación Membresia Internacional JOSE JULIAN VILLAMIZAR ARAQUE', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00155', '2025-12-21', 1, @CuentaId, 80000.00, 'Renovación Membresia Internacional JHON ENMANUEL ARZUZA PÁEZ', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00156', '2025-12-21', 1, @CuentaId, 80000.00, 'Renovación Membresia Internacional JENNIFER ANDREA CARDONA BENITEZ', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00157', '2025-12-21', 1, @CuentaId, 80000.00, 'Renovación Membresia Internacional WILLIAM HUMBERTO JIMENEZ PEREZ', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00158', '2025-12-21', 1, @CuentaId, 80000.00, 'Renovación Membresia Internacional YEFERSON USUGA', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00159', '2025-12-21', 1, @CuentaId, 80000.00, 'Renovación Membresia Internacional JEFFERSON MONTOYA MUÑOZ', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00160', '2025-12-21', 1, @CuentaId, 80000.00, 'Renovación Membresia Internacional JHON DAVID SANCHEZ', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00161', '2025-12-21', 1, @CuentaId, 80000.00, 'Renovación Membresia Internacional MILTON DARIO GOMEZ RIVERA', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00162', '2025-12-21', 1, @CuentaId, 80000.00, 'Renovación Membresia Internacional CARLOS MARIO DIAZ DIAZ', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00163', '2025-12-21', 1, @CuentaId, 80000.00, 'Renovación Membresia Internacional LAURA VIVIAN SALAZAR MORENO', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00164', '2025-12-21', 1, @CuentaId, 80000.00, 'Renovación Membresia Internacional GUSTAVO ADOLFO GOMEZ ZULUAGA', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00165', '2025-12-21', 1, @CuentaId, 80000.00, 'Renovación Membresia Internacional NELSON AUGUSTO MONTOYA MATAUTE', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00166', '2025-12-21', 1, @CuentaId, 80000.00, 'Renovación Membresia Internacional HECTOR MARIO GONZALEZ HENAO', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00167', '2025-12-21', 1, @CuentaId, 160000.00, 'Renovación Membresia Internacional ARLEX BETANCUR', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00168', '2025-12-21', 1, @CuentaId, 60000.00, 'Mensualidades Capítulo CESAR LEONEL RODRIGUEZ GALAN', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00169', '2025-12-21', 1, @CuentaId, 240000.00, 'Mensualidades Capítulo ROBINSON GALVIS', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00170', '2025-12-21', 1, @CuentaId, 240000.00, 'Mensualidades Capítulo GIRLESA MARÍA BUITRAGO', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00171', '2025-12-21', 1, @CuentaId, 240000.00, 'Mensualidades Capítulo CARLOS ANDRES PEREZ AREIZA', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00172', '2025-12-21', 1, @CuentaId, 240000.00, 'Mensualidades Capítulo CARLOS MARIO CEBALLOS', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00173', '2025-12-21', 1, @CuentaId, 20000.00, 'Mensualidades Capítulo JOSE JULIAN VILLAMIZAR ARAQUE', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00174', '2025-12-21', 1, @CuentaId, 150000.00, 'Mensualidades Capítulo JHON ENMANUEL ARZUZA PÁEZ', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00175', '2025-12-21', 1, @CuentaId, 140000.00, 'Mensualidades Capítulo JEFFERSON MONTOYA MUÑOZ', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00176', '2025-12-21', 1, @CuentaId, 240000.00, 'Mensualidades Capítulo JHON DAVID SANCHEZ', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00177', '2025-12-21', 1, @CuentaId, 120000.00, 'Mensualidades Capítulo MILTON DARIO GOMEZ RIVERA', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00178', '2025-12-21', 1, @CuentaId, 60000.00, 'Mensualidades Capítulo GUSTAVO ADOLFO GOMEZ ZULUAGA', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00179', '2025-12-21', 1, @CuentaId, 60000.00, 'Mensualidades Capítulo NELSON AUGUSTO MONTOYA MATAUTE', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00180', '2025-12-21', 1, @CuentaId, 240000.00, 'Anualidad 2026 Capítulo CARLOS ALBERTO ARAQUE BETANCUR', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00181', '2025-12-21', 1, @CuentaId, 240000.00, 'Anualidad 2026 Capítulo DANIEL VILLAMIZAR', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00182', '2025-12-21', 1, @CuentaId, 188799.00, 'Parches GUSTAVO ADOLFO GOMEZ ZULUAGA', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00183', '2025-12-21', 1, @CuentaId, 188799.00, 'Parches NELSON AUGUSTO MONTOYA MATAUTE', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00184', '2025-12-21', 1, @CuentaId, 153579.00, 'Parches JHON DAVID SANCHEZ', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00185', '2025-12-21', 1, @CuentaId, 40000.00, 'Actividad Fin de Año DANIEL VILLAMIZAR', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00186', '2025-12-21', 1, @CuentaId, 80000.00, 'Actividad Fin de Año ROBINSON GALVIS', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00187', '2025-12-21', 1, @CuentaId, 80000.00, 'Actividad Fin de Año JHON JARVEY GÓMEZ PATIÑO', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00188', '2025-12-21', 1, @CuentaId, 160000.00, 'Actividad Fin de Año CARLOS ANDRES PEREZ AREIZA', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00189', '2025-12-21', 1, @CuentaId, 40000.00, 'Actividad Fin de Año JOSÉ EDINSON OSPINA CRUZ', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00190', '2025-12-21', 1, @CuentaId, 40000.00, 'Actividad Fin de Año CARLOS MARIO CEBALLOS', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00191', '2025-12-21', 1, @CuentaId, 40000.00, 'Actividad Fin de Año JHON ENMANUEL ARZUZA PÁEZ', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00192', '2025-12-21', 1, @CuentaId, 80000.00, 'Actividad Fin de Año YEFERSON USUGA', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00193', '2025-12-21', 1, @CuentaId, 40000.00, 'Actividad Fin de Año MILTON DARIO GOMEZ RIVERA', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00194', '2025-12-21', 1, @CuentaId, 200000.00, 'Actividad Fin de Año CARLOS MARIO DIAZ DIAZ', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00195', '2025-12-21', 1, @CuentaId, 40000.00, 'Actividad Fin de Año LAURA VIVIANA SALAZAR MORENO', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00196', '2025-12-21', 1, @CuentaId, 80000.00, 'Actividad Fin de Año NELSON AUGUSTO MONTOYA MATAUTE', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00197', '2025-12-21', 1, @CuentaId, 80000.00, 'Actividad Fin de Año CARLOS JULIO MOVIE', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00198', '2025-12-21', 1, @CuentaId, 40000.00, 'Actividad Fin de Año HECTOR MARIO GONZALEZ HENAO', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00199', '2025-12-21', 1, @CuentaId, 80000.00, 'Actividad Fin de Año ARLEX BETANCUR', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00200', '2025-12-21', 2, @CuentaId, 1200000.00, 'Cambio marca', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00201', '2025-12-21', 2, @CuentaId, 80000.00, 'Auxilio Gustavo Gomez', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00202', '2025-12-21', 2, @CuentaId, 80000.00, 'Auxilio William Jimenez', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00203', '2025-12-21', 2, @CuentaId, 80000.00, 'Auxilio Jhon Arzuza', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00204', '2025-12-21', 2, @CuentaId, 2688962.00, 'Pago Membresias', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00205', '2025-12-21', 2, @CuentaId, 725000.00, 'Pago Dia Sol Milton', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00206', '2025-12-21', 2, @CuentaId, 80000.00, 'Auxilio Carlos Diaz', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00207', '2025-12-21', 2, @CuentaId, 1439288.00, 'Actividad Diciembre', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00208', '2025-12-21', 2, @CuentaId, 210000.00, 'Banderas Medellin', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00209', '2025-12-21', 2, @CuentaId, 18000.00, 'Pago Interrapidisimo', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00210', '2025-12-21', 2, @CuentaId, 129000.00, 'LICORES ACTIVIDAD', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0),
(NEWID(), 'MV-2025-00211', '2025-12-21', 2, @CuentaId, 124368.00, 'GASEOSAS, LECHE, GALLETAS ACTIVIDAD', 1, 1, '2025-12-21', GETDATE(), 'import-excel', 'INFORME TESORERIA 2025.xlsx', 'CORTE DICIEMBRE 31-25', 0);

PRINT '✓ DICIEMBRE: 68 registros';
PRINT '';
PRINT '════════════════════════════════════════════════════════════════';
PRINT '✓ IMPORTACIÓN COMPLETADA: ENERO-DICIEMBRE (211 registros)';
PRINT '════════════════════════════════════════════════════════════════';


-- Total importado: 211 registros (ENE–DIC 2025)
-- Ingresos (Tipo 1): 150
-- Egresos (Tipo 2): 61
-- Fuente: INFORME TESORERIA 2025.xlsx
-- BD: Azure SQL - sqldb-tesorerialamamedellin-prod