using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;

namespace Server.Services.ConciliacionBancaria;

/// <summary>
/// Enum para formatos de CSV bancario soportados
/// </summary>
public enum BankCsvFormat
{
    Desconocido,
    Extracto,
    Movimientos
}

/// <summary>
/// Servicio para detectar automáticamente el formato de un CSV bancario.
/// Soporta 2 formatos: Extracto (E*.CSV) y Movimientos (CSV_*.csv).
/// </summary>
public interface IBankCsvFormatDetector
{
    /// <summary>
    /// Detecta el formato del CSV analizando headers y estructura (Stream async)
    /// </summary>
    Task<BankCsvFormat> DetectFormatAsync(Stream csvStream);
    
    /// <summary>
    /// Detecta el formato del CSV analizando headers y estructura
    /// </summary>
    string DetectFormat(string[] primerasLineas);

    /// <summary>
    /// Valida si un CSV es válido para procesamiento
    /// </summary>
    bool IsValidCsv(string[] primerasLineas);
}

public class BankCsvFormatDetector : IBankCsvFormatDetector
{
    /// <summary>
    /// Detecta el formato desde un Stream
    /// </summary>
    public async Task<BankCsvFormat> DetectFormatAsync(Stream csvStream)
    {
        csvStream.Position = 0;
        using var reader = new StreamReader(csvStream, Encoding.UTF8, leaveOpen: true);
        
        var lineas = new List<string>();
        for (int i = 0; i < 10 && !reader.EndOfStream; i++)
        {
            var linea = await reader.ReadLineAsync();
            if (!string.IsNullOrWhiteSpace(linea))
                lineas.Add(linea);
        }
        
        csvStream.Position = 0; // Resetear para posterior lectura
        
        var formato = DetectFormat(lineas.ToArray());
        return formato switch
        {
            "Extracto" => BankCsvFormat.Extracto,
            "Movimientos" => BankCsvFormat.Movimientos,
            _ => BankCsvFormat.Desconocido
        };
    }

    /// <summary>
    /// Patrones de columnas esperados para formato Extracto
    /// Ejemplo: Fecha|Cuenta|Secuencia|Tipo|SubTipo|Comprobante|Concepto|Referencia|Valor|SaldoAnt|Signo|...
    /// </summary>
    private static readonly string[] ExtractoHeaders = new[]
    {
        "fecha", "cuenta", "secuencia", "tipo", "subtipo", "comprobante", "concepto", "referencia"
    };

    /// <summary>
    /// Patrones de columnas esperados para formato Movimientos
    /// Ejemplo: Cuenta|SubTipo|Comprobante|Concepto|CodigoRef|Valor|...
    /// </summary>
    private static readonly string[] MovimientosHeaders = new[]
    {
        "cuenta", "subtipo", "comprobante", "concepto", "valor"
    };

    public string DetectFormat(string[] primerasLineas)
    {
        if (primerasLineas.Length == 0)
            return "Desconocido";

        // Obtener la primera línea (header o primer registro)
        string primerLinea = primerasLineas[0].Trim();

        // Contar campos separados por comas o puntos y comas
        var campos = primerLinea.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
        int cantidadCampos = campos.Length;

        // Formato Extracto: 15+ campos (más columnas que Movimientos)
        // Formato Movimientos: 9-12 campos (menos columnas)
        if (cantidadCampos >= 12)
        {
            // Validar por patrones: buscar "SALDO DIA", "SALDO INICIAL", "SALDO FINAL" (típicos del Extracto)
            for (int i = 0; i < primerasLineas.Length && i < 10; i++)
            {
                if (primerasLineas[i].Contains("SALDO", StringComparison.OrdinalIgnoreCase))
                    return "Extracto";
            }
            return "Extracto";
        }
        else
        {
            // Movimientos tiene menos campos
            return "Movimientos";
        }
    }

    public bool IsValidCsv(string[] primerasLineas)
    {
        if (primerasLineas.Length == 0)
            return false;

        // Validaciones básicas
        string primerLinea = primerasLineas[0].Trim();
        if (string.IsNullOrWhiteSpace(primerLinea))
            return false;

        // Debe tener al menos 1 separador (coma o punto y coma)
        if (!primerLinea.Contains(',') && !primerLinea.Contains(';'))
            return false;

        return true;
    }
}
