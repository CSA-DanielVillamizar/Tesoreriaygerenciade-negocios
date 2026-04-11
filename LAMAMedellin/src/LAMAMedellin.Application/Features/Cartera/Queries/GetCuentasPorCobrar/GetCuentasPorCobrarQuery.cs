using LAMAMedellin.Domain.Enums;
using MediatR;

namespace LAMAMedellin.Application.Features.Cartera.Queries.GetCuentasPorCobrar;

public sealed record GetCuentasPorCobrarQuery(
    EstadoCuentaPorCobrar? Estado = null,
    Guid? MiembroId = null)
    : IRequest<List<CuentaPorCobrarDto>>;
