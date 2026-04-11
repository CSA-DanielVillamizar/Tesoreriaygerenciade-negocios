using LAMAMedellin.Application.Common.Interfaces.Repositories;
using MediatR;

namespace LAMAMedellin.Application.Features.Cartera.Queries.GetCuentasPorCobrar;

public sealed class GetCuentasPorCobrarQueryHandler(
    ICuentaPorCobrarRepository cuentaPorCobrarRepository)
    : IRequestHandler<GetCuentasPorCobrarQuery, List<CuentaPorCobrarDto>>
{
    public async Task<List<CuentaPorCobrarDto>> Handle(
        GetCuentasPorCobrarQuery request,
        CancellationToken cancellationToken)
    {
        var cuentas = await cuentaPorCobrarRepository
            .GetListadoAsync(request.Estado, request.MiembroId, cancellationToken);

        return cuentas
            .OrderByDescending(c => c.FechaVencimiento)
            .ThenBy(c => c.Miembro!.Nombres)
            .ThenBy(c => c.Miembro!.Apellidos)
            .Select(c => new CuentaPorCobrarDto(
                c.Id,
                $"{c.Miembro!.Nombres} {c.Miembro!.Apellidos}".Trim(),
                c.ConceptoCobro?.Nombre ?? string.Empty,
                c.FechaEmision,
                c.FechaVencimiento,
                c.ValorTotal,
                c.SaldoPendiente,
                c.Estado))
            .ToList();
    }
}
