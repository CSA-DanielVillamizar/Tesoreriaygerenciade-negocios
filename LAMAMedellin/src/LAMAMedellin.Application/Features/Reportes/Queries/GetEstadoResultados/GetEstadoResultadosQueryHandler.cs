using LAMAMedellin.Application.Common.Interfaces.Repositories;
using MediatR;

namespace LAMAMedellin.Application.Features.Reportes.Queries.GetEstadoResultados;

public sealed class GetEstadoResultadosQueryHandler(
    IIngresoRepository ingresoRepository,
    IEgresoRepository egresoRepository)
    : IRequestHandler<GetEstadoResultadosQuery, EstadoResultadosDto>
{
    public async Task<EstadoResultadosDto> Handle(
        GetEstadoResultadosQuery request,
        CancellationToken cancellationToken)
    {
        if (request.FechaInicio > request.FechaFin)
        {
            throw new ArgumentException("FechaInicio no puede ser mayor que FechaFin.");
        }

        var ingresos = await ingresoRepository.GetByFechaRangoAsync(
            request.FechaInicio,
            request.FechaFin,
            cancellationToken);

        var egresos = await egresoRepository.GetByFechaRangoAsync(
            request.FechaInicio,
            request.FechaFin,
            cancellationToken);

        var totalIngresos = ingresos.Sum(x => x.Monto);
        var totalEgresos = egresos.Sum(x => x.Monto);

        var totalesIngresos = ingresos
            .GroupBy(x => x.Concepto)
            .Select(g => new DetalleEstadoResultadosDto(
                "Ingreso",
                g.Key,
                g.Sum(x => x.Monto)));

        var totalesEgresos = egresos
            .GroupBy(x => x.Concepto)
            .Select(g => new DetalleEstadoResultadosDto(
                "Egreso",
                g.Key,
                g.Sum(x => x.Monto)));

        var totalesPorConcepto = totalesIngresos
            .Concat(totalesEgresos)
            .OrderBy(x => x.TipoMovimiento)
            .ThenByDescending(x => x.Total)
            .ToList();

        return new EstadoResultadosDto(
            totalIngresos,
            totalEgresos,
            totalIngresos - totalEgresos,
            totalesPorConcepto);
    }
}
