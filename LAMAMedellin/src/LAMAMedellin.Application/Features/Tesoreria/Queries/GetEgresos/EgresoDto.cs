namespace LAMAMedellin.Application.Features.Tesoreria.Queries.GetEgresos;

public sealed record EgresoDto(
    Guid Id,
    DateTime Fecha,
    decimal Monto,
    string Concepto,
    Guid? TerceroId,
    Guid CuentaContableId,
    string CuentaContableNombre,
    Guid CajaId,
    string CajaNombre,
    Guid? ComprobanteContableId);
