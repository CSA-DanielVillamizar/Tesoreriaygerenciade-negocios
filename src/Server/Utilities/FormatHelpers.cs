using System.Globalization;

namespace Server.Utilities;

/// <summary>
/// Helpers centralizados para formateo consistente en toda la aplicación.
/// </summary>
public static class FormatHelpers
{
    private static readonly CultureInfo ColombiaCulture = new("es-CO");
    
    /// <summary>
    /// Formatea un valor decimal como moneda COP con formato colombiano.
    /// Ejemplo: 1000000 → "$1.000.000"
    /// </summary>
    public static string FormatCop(decimal value) => value.ToString("C0", ColombiaCulture);
    
    /// <summary>
    /// Formatea un valor decimal con separador de miles sin símbolo de moneda.
    /// Ejemplo: 1000000 → "$1,000,000"
    /// </summary>
    public static string FormatNumber(decimal value) => $"${value:N0}";
}
