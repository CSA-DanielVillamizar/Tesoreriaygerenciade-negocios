using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Server.Utilities;

/// <summary>
/// Helper para construir query strings de forma robusta sin duplicar ? y & 
/// </summary>
public static class QueryStringHelper
{
    /// <summary>
    /// Construye un query string a partir de parámetros opcionales.
    /// Ejemplo: BuildQuery("api/presupuestos/estadisticas", ("ano", "2026"), ("mes", "11"))
    /// Resultado: "api/presupuestos/estadisticas?ano=2026&mes=11"
    /// </summary>
    /// <param name="baseUrl">URL base sin query string</param>
    /// <param name="parameters">Tuplas de (clave, valor). Valores null/vacíos se ignoran</param>
    /// <returns>URL completa con query string, o URL original si no hay parámetros válidos</returns>
    public static string BuildQuery(string baseUrl, params (string key, string? value)[] parameters)
    {
        ArgumentNullException.ThrowIfNull(baseUrl);

        var queryParts = new List<string>();

        foreach (var (key, value) in parameters)
        {
            // Ignorar parámetros vacíos o null
            if (!string.IsNullOrWhiteSpace(value))
            {
                var encodedKey = Uri.EscapeDataString(key);
                var encodedValue = Uri.EscapeDataString(value);
                queryParts.Add($"{encodedKey}={encodedValue}");
            }
        }

        if (queryParts.Count == 0)
            return baseUrl;

        return $"{baseUrl}?{string.Join("&", queryParts)}";
    }

    /// <summary>
    /// Construye un query string a partir de un diccionario de parámetros.
    /// </summary>
    /// <param name="baseUrl">URL base sin query string</param>
    /// <param name="parameters">Diccionario de parámetros. Valores null/vacíos se ignoran</param>
    /// <returns>URL completa con query string, o URL original si no hay parámetros válidos</returns>
    public static string BuildQuery(string baseUrl, Dictionary<string, string?>? parameters)
    {
        ArgumentNullException.ThrowIfNull(baseUrl);

        if (parameters == null || parameters.Count == 0)
            return baseUrl;

        var queryParts = new List<string>();

        foreach (var kvp in parameters)
        {
            if (!string.IsNullOrWhiteSpace(kvp.Value))
            {
                var encodedKey = Uri.EscapeDataString(kvp.Key);
                var encodedValue = Uri.EscapeDataString(kvp.Value);
                queryParts.Add($"{encodedKey}={encodedValue}");
            }
        }

        if (queryParts.Count == 0)
            return baseUrl;

        return $"{baseUrl}?{string.Join("&", queryParts)}";
    }

    /// <summary>
    /// Construye un query string para parámetros opcionales de tipo int?
    /// </summary>
    /// <param name="baseUrl">URL base sin query string</param>
    /// <param name="parameters">Tuplas de (clave, valor int?). Valores null se ignoran</param>
    /// <returns>URL completa con query string, o URL original si no hay parámetros válidos</returns>
    public static string BuildQueryInt(string baseUrl, params (string key, int? value)[] parameters)
    {
        ArgumentNullException.ThrowIfNull(baseUrl);

        var queryParts = new List<string>();

        foreach (var (key, value) in parameters)
        {
            // Solo agregar si el valor es no-null
            if (value.HasValue)
            {
                var encodedKey = Uri.EscapeDataString(key);
                queryParts.Add($"{encodedKey}={value.Value}");
            }
        }

        if (queryParts.Count == 0)
            return baseUrl;

        return $"{baseUrl}?{string.Join("&", queryParts)}";
    }
}
