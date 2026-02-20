using Server.DTOs.ConciliacionBancaria;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Server.Services.ConciliacionBancaria;

/// <summary>
/// Interface para parsers de CSV bancario
/// </summary>
public interface IBankCsvParser
{
    /// <summary>
    /// Parsea un archivo CSV y normaliza a MovimientoExtractoDto
    /// </summary>
    Task<CsvImportResultDto> ParseAsync(Stream csvStream, string nombreArchivo);
}

/// <summary>
/// Parser para formato Extracto (E*.CSV)
/// Ejemplo línea: 20251201, 230-000137-74, 1, 7, 230, 0, SALDO INICIAL, 000000, .00, 0, C
/// </summary>
public class ExtractoCsvParser : IBankCsvParser
{
    private const char SEPARATOR = ',';

    public async Task<CsvImportResultDto> ParseAsync(Stream csvStream, string nombreArchivo)
    {
        var resultado = new CsvImportResultDto
        {
            NombreArchivo = nombreArchivo,
            Formato = "Extracto"
        };

        try
        {
            csvStream.Position = 0;
            using var reader = new StreamReader(csvStream, Encoding.UTF8, leaveOpen: true);
            var contenido = await reader.ReadToEndAsync();
            var lineas = contenido.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            
            resultado.TotalLineas = lineas.Length;
            decimal? saldoInicial = null;

            for (int i = 0; i < lineas.Length; i++)
            {
                string linea = lineas[i].Trim();
                if (string.IsNullOrWhiteSpace(linea)) continue;

                try
                {
                    var campos = linea.Split(SEPARATOR);
                    if (campos.Length < 9) continue; // Necesita al menos 9 campos para campos[8]

                    string fechaStr = campos[0].Trim();
                    string concepto = campos[6].Trim();
                    string referencia = campos[7].Trim();
                    string valorStr = campos[8].Trim();
                    string signoStr = campos.Length > 10 ? campos[10].Trim() : "C";

                    // Ignorar líneas SALDO
                    if (concepto.Contains("SALDO", StringComparison.OrdinalIgnoreCase))
                    {
                        if (concepto.Contains("INICIAL", StringComparison.OrdinalIgnoreCase))
                        {
                            if (decimal.TryParse(valorStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var saldo))
                                saldoInicial = saldo;
                        }
                        else if (concepto.Contains("FINAL", StringComparison.OrdinalIgnoreCase) || concepto.Contains("DIA", StringComparison.OrdinalIgnoreCase))
                        {
                            if (decimal.TryParse(valorStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var saldo))
                                resultado.SaldoFinal = saldo;
                        }
                        continue;
                    }

                    // Parsear fecha
                    if (!DateTime.TryParseExact(fechaStr, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fecha))
                        fecha = DateTime.UtcNow;

                    // Parsear valor
                    if (!decimal.TryParse(valorStr.Replace(".", ","), NumberStyles.Any, CultureInfo.GetCultureInfo("es-CO"), out var valor))
                        valor = 0;

                    // Aplicar signo
                    bool esIngreso = signoStr != "D" && signoStr != "-";
                    if (!esIngreso && valor > 0)
                        valor = -valor;

                    resultado.Movimientos.Add(new MovimientoExtractoDto
                    {
                        Fecha = fecha,
                        Descripcion = concepto,
                        Monto = Math.Abs(valor),
                        EsIngreso = esIngreso,
                        Referencia = !string.IsNullOrWhiteSpace(referencia) ? referencia : null,
                        NumeroLinea = i + 1
                    });

                    resultado.LineasProcesadas++;
                }
                catch (Exception ex)
                {
                    resultado.Errores.Add($"Línea {i + 1}: {ex.Message}");
                    resultado.LineasConError++;
                }
            }

            if (saldoInicial.HasValue)
                resultado.SaldoInicial = saldoInicial;
        }
        catch (Exception ex)
        {
            resultado.Errores.Add($"Error global: {ex.Message}");
        }

        return resultado;
    }
}

/// <summary>
/// Parser para formato Movimientos (CSV_*.csv)
/// Ejemplo: 230-000137-74, 230, , 20260123, , 20000.00, 4160, TRANSFERENCIA CTA SUC VIRTUAL, 0
/// </summary>
public class MovimientosCsvParser : IBankCsvParser
{
    private const char SEPARATOR = ',';

    public async Task<CsvImportResultDto> ParseAsync(Stream csvStream, string nombreArchivo)
    {
        var resultado = new CsvImportResultDto
        {
            NombreArchivo = nombreArchivo,
            Formato = "Movimientos"
        };

        try
        {
            csvStream.Position = 0;
            using var reader = new StreamReader(csvStream, Encoding.UTF8, leaveOpen: true);
            var contenido = await reader.ReadToEndAsync();
            var lineas = contenido.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            
            resultado.TotalLineas = lineas.Length;

            for (int i = 0; i < lineas.Length; i++)
            {
                string linea = lineas[i].Trim();
                if (string.IsNullOrWhiteSpace(linea)) continue;

                try
                {
                    var campos = linea.Split(SEPARATOR);
                    if (campos.Length < 8) continue;

                    string cuenta = campos[0].Trim();
                    string fechaStr = campos[3].Trim();
                    string valorStr = campos[5].Trim();
                    string codigoRef = campos[6].Trim();
                    string concepto = campos[7].Trim();

                    if (string.IsNullOrWhiteSpace(fechaStr)) continue;

                    // Parsear fecha
                    if (!DateTime.TryParseExact(fechaStr, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fecha))
                        fecha = DateTime.UtcNow;

                    // Parsear valor
                    if (!decimal.TryParse(valorStr.Replace(".", ","), NumberStyles.Any, CultureInfo.GetCultureInfo("es-CO"), out var valor))
                        valor = 0;

                    bool esIngreso = valor >= 0;

                    resultado.Movimientos.Add(new MovimientoExtractoDto
                    {
                        Fecha = fecha,
                        Descripcion = concepto,
                        Monto = Math.Abs(valor),
                        EsIngreso = esIngreso,
                        Referencia = !string.IsNullOrWhiteSpace(codigoRef) ? codigoRef : null,
                        NumeroLinea = i + 1
                    });

                    resultado.LineasProcesadas++;
                }
                catch (Exception ex)
                {
                    resultado.Errores.Add($"Línea {i + 1}: {ex.Message}");
                    resultado.LineasConError++;
                }
            }
        }
        catch (Exception ex)
        {
            resultado.Errores.Add($"Error global: {ex.Message}");
        }

        return resultado;
    }
}
