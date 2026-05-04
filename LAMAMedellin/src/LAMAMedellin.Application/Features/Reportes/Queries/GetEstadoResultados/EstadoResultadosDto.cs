namespace LAMAMedellin.Application.Features.Reportes.Queries.GetEstadoResultados;

public sealed record EstadoResultadosDto(
    decimal TotalIngresos,
    decimal TotalEgresos,
    decimal BalanceNeto,
    IReadOnlyList<DetalleEstadoResultadosDto> TotalesPorConcepto);

public sealed record DetalleEstadoResultadosDto(
    string TipoMovimiento,
    string Concepto,
    decimal Total);
