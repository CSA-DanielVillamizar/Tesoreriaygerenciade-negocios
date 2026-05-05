using LAMAMedellin.Application.Common.Interfaces.Repositories;
using MediatR;

namespace LAMAMedellin.Application.Features.Dashboard.Queries.GetResumen;

public sealed class GetResumenDashboardQueryHandler(
    IMiembroRepository miembroRepository,
    ICajaRepository cajaRepository,
    IEventoRepository eventoRepository)
    : IRequestHandler<GetResumenDashboardQuery, DashboardResumenDto>
{
    public async Task<DashboardResumenDto> Handle(
        GetResumenDashboardQuery request,
        CancellationToken cancellationToken)
    {
        var totalMiembrosActivosTask = miembroRepository.CountActivosAsync(cancellationToken);
        var totalDineroCajasTask = cajaRepository.GetTotalSaldoActualAsync(cancellationToken);
        var proximoEventoTask = eventoRepository.GetProximoProgramadoAsync(cancellationToken);

        await Task.WhenAll(totalMiembrosActivosTask, totalDineroCajasTask, proximoEventoTask);

        var proximoEvento = await proximoEventoTask;

        return new DashboardResumenDto(
            await totalMiembrosActivosTask,
            await totalDineroCajasTask,
            proximoEvento?.Nombre,
            proximoEvento?.FechaProgramada);
    }
}
