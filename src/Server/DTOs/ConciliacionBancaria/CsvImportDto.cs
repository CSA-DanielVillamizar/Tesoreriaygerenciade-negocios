namespace Server.DTOs.ConciliacionBancaria;

/// <summary>
/// DTO para importación de extracto bancario CSV
/// </summary>
public class CsvImportResultDto
{
    public string NombreArchivo { get; set; } = string.Empty;
    public string Formato { get; set; } = string.Empty;
    public int TotalLineas { get; set; }
    public int LineasProcesadas { get; set; }
    public int LineasConError { get; set; }
    public decimal? SaldoInicial { get; set; }
    public decimal? SaldoFinal { get; set; }
    public List<MovimientoExtractoDto> Movimientos { get; set; } = new();
    public List<string> Errores { get; set; } = new();
}

/// <summary>
/// DTO para movimiento normalizado de extracto
/// </summary>
public class MovimientoExtractoDto
{
    public DateTime Fecha { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public decimal Monto { get; set; }
    public bool EsIngreso { get; set; }
    public string? Referencia { get; set; }
    public int NumeroLinea { get; set; }
}
