using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;
using Server.Services.Import; // Asegúrate de tener este using
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Server.Controllers;

[ApiController]
[Route("api/admin")]
[AllowAnonymous]
public class AdminOpsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<AdminOpsController> _logger;
    private readonly IExcelTreasuryImportService _importService; // Inyectamos el servicio

    public AdminOpsController(
        AppDbContext context, 
        ILogger<AdminOpsController> logger,
        IExcelTreasuryImportService importService)
    {
        _context = context;
        _logger = logger;
        _importService = importService;
    }

    [HttpPost("reset-and-reseed")]
    public async Task<IActionResult> ResetAndReseed([FromForm] IFormFile? excelFile)
    {
        _logger.LogWarning("[ADMIN-OPS] Iniciando Hard Reset por usuario {User}", User.Identity?.Name ?? "Sistema");

        try
        {
            var resumen = new
            {
                RecibosEliminados = 0,
                EgresosEliminados = 0,
                MovimientosEliminados = 0,
                CierresEliminados = 0,
                SaldoInicialCreado = false, // Nuevo indicador
                ImportSummary = new ImportSummary()
            };

            // ========== FASE 1: LIMPIEZA (WIPE) ==========
            _logger.LogInformation("[WIPE] Fase de limpieza iniciada");

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Orden correcto de borrado por FKs
                var recibosCount = await _context.Database.ExecuteSqlRawAsync("DELETE FROM Recibos");
                resumen = resumen with { RecibosEliminados = recibosCount };
                
                var egresosCount = await _context.Database.ExecuteSqlRawAsync("DELETE FROM Egresos");
                resumen = resumen with { EgresosEliminados = egresosCount };
                
                var movimientosCount = await _context.Database.ExecuteSqlRawAsync("DELETE FROM MovimientosTesoreria");
                resumen = resumen with { MovimientosEliminados = movimientosCount };
                
                var cierresCount = await _context.Database.ExecuteSqlRawAsync("DELETE FROM CierresMensuales");
                resumen = resumen with { CierresEliminados = cierresCount };

                await transaction.CommitAsync();
                _logger.LogInformation("[WIPE] Fase de limpieza completada exitosamente");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "[WIPE] Error durante limpieza, rollback ejecutado");
                return StatusCode(500, new { error = "Error en fase de limpieza", detalle = ex.Message });
            }

            // ========== FASE 2: INYECCIÓN SALDO INICIAL (MANUAL) ==========
            // Esto garantiza que Enero arranque con plata antes de leer el Excel
            try 
            {
                _logger.LogWarning("💰 Insertando SALDO INICIAL MANUALMENTE para ENERO 2025");
                
                var cuentaPrincipal = await _context.CuentasFinancieras
                    .FirstOrDefaultAsync(c => c.Codigo == "BANCO-BCOL-001");

                // Buscamos fuente "OTROS" o la primera que haya
                var fuenteOtros = await _context.FuentesIngreso
                    .FirstOrDefaultAsync(f => f.Codigo == "OTROS") 
                    ?? await _context.FuentesIngreso.FirstOrDefaultAsync();

                if (cuentaPrincipal != null && fuenteOtros != null)
                {
                    var saldoInicial = new MovimientoTesoreria
                    {
                        Id = Guid.NewGuid(),
                        NumeroMovimiento = "SALDO-INICIAL-2025-MANUAL",
                        Fecha = new DateTime(2025, 1, 1),
                        Tipo = TipoMovimientoTesoreria.Ingreso,
                        CuentaFinancieraId = cuentaPrincipal.Id,
                        FuenteIngresoId = fuenteOtros.Id,
                        CategoriaEgresoId = null,
                        Valor = 6_915_000m, // Valor exacto de tu Excel
                        Descripcion = "SALDO INICIAL 2025 (Carga Manual)",
                        Medio = MedioPagoTesoreria.Transferencia,
                        Estado = EstadoMovimientoTesoreria.Aprobado,
                        FechaAprobacion = new DateTime(2025, 1, 1),
                        UsuarioAprobacion = "SYSTEM",
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = "SYSTEM",
                        ImportHash = "HASH-SALDO-INICIAL-2025",
                        ImportSource = "MANUAL-INJECTION",
                        ImportSheet = "ENERO",
                        ImportRowNumber = 0,
                        ImportedAtUtc = DateTime.UtcNow,
                        ImportBalanceExpected = 6_915_000m
                    };

                    _context.MovimientosTesoreria.Add(saldoInicial);
                    await _context.SaveChangesAsync();
                    resumen = resumen with { SaldoInicialCreado = true };
                    _logger.LogInformation("✅ Saldo Inicial de $6,915,000 creado exitosamente.");
                }
                else
                {
                    _logger.LogError("❌ No se pudo crear Saldo Inicial: Falta cuenta o fuente de ingreso.");
                }
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex, "❌ Error crítico creando saldo inicial manual.");
                 // No detenemos el proceso, intentamos importar el resto
            }

            // ========== FASE 3: IMPORTACIÓN EXCEL ==========
            if (excelFile != null && excelFile.Length > 0)
            {
                await using var stream = excelFile.OpenReadStream();
                // Llamamos al servicio de importación que ya tienes (pero ahora está simplificado)
                var importResult = await _importService.ImportAsync(stream, excelFile.FileName, dryRun: false);
                
                return Ok(new { 
                    Message = "Hard Reset & Seed completado",
                    WipeStats = resumen,
                    ImportStats = importResult
                });
            }

            return Ok(new { Message = "Reset completado (Sin archivo Excel para importar)", Stats = resumen });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[ADMIN-OPS] Error crítico global");
            return StatusCode(500, new { error = "Error crítico", detalle = ex.Message });
        }
    }

    /// <summary>
    /// REPARACIÓN CRÍTICA: Sincroniza las tablas legacy (Egresos/Recibos) 
    /// usando los datos ya existentes en MovimientosTesoreria.
    /// Crea ReciboItems para que los ingresos aparezcan en los reportes.
    /// </summary>
    [HttpPost("sync-legacy")]
    public async Task<IActionResult> SyncLegacyTables()
    {
        _logger.LogWarning("🔄 Iniciando sincronización con reconstrucción de Items...");
        
        // Obtener un concepto genérico para asignar a los items (usamos el primero disponible)
        var conceptoGenerico = await _context.Conceptos.FirstOrDefaultAsync();
        if (conceptoGenerico == null)
        {
            return BadRequest(new { Error = "No hay conceptos disponibles en la base de datos. Debe crear al menos un concepto primero." });
        }

        var movimientos = await _context.MovimientosTesoreria.ToListAsync();
        int recibosConItems = 0;

        foreach (var mov in movimientos.Where(m => m.Tipo == TipoMovimientoTesoreria.Ingreso))
        {
            // 1. Buscar el recibo que ya existe
            var recibo = await _context.Recibos.FirstOrDefaultAsync(r => r.ImportRowHash == mov.ImportHash);
            
            if (recibo != null)
            {
                // 2. Verificar si ya tiene items para no duplicar
                var tieneItems = await _context.ReciboItems.AnyAsync(ri => ri.ReciboId == recibo.Id);
                
                if (!tieneItems)
                {
                    // 3. Truncar descripción para evitar errores
                    var conceptoTexto = mov.Descripcion?.Length > 200 
                        ? mov.Descripcion.Substring(0, 200) 
                        : mov.Descripcion ?? "Ingreso";

                    // 4. Crear el Item (esto es lo que suma el Reporte)
                    _context.ReciboItems.Add(new ReciboItem
                    {
                        // Id se auto-genera (IDENTITY)
                        ReciboId = recibo.Id,
                        ConceptoId = conceptoGenerico.Id, // Usar concepto genérico
                        Cantidad = 1,
                        PrecioUnitarioMonedaOrigen = mov.Valor,
                        MonedaOrigen = Moneda.COP,
                        SubtotalCop = mov.Valor, // <--- El reporte lee este campo
                        Notas = conceptoTexto // Guardamos la descripción original aquí
                    });
                    recibosConItems++;
                }
            }
        }

        await _context.SaveChangesAsync();
        _logger.LogInformation($"✅ Sincronización de Items completada: {recibosConItems} items creados usando concepto '{conceptoGenerico.Nombre}'");
        
        return Ok(new 
        { 
            Message = "Sincronización de Items completada", 
            ItemsCreados = recibosConItems,
            ConceptoUsado = conceptoGenerico.Nombre
        });
    }
}