using LAMAMedellin.Domain.Enums;

namespace LAMAMedellin.Application.Features.Cartera.Queries.GetCuentasPorCobrar;

public sealed record CuentaPorCobrarDto(
    Guid Id,
    string NombreCompletoMiembro,
    string NombreConcepto,
    DateOnly FechaEmision,
    DateOnly FechaVencimiento,
    decimal ValorTotal,
    decimal SaldoPendiente,
    EstadoCuentaPorCobrar Estado);
