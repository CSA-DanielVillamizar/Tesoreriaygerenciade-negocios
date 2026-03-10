using LAMAMedellin.Application.Common.Interfaces.Repositories;
using MediatR;

namespace LAMAMedellin.Application.Features.Configuracion.Tarifas.Queries.GetTarifasCuota;

public sealed class GetTarifasCuotaQueryHandler(ITarifaCuotaRepository tarifaCuotaRepository)
    : IRequestHandler<GetTarifasCuotaQuery, List<TarifaCuotaDto>>
{
    public async Task<List<TarifaCuotaDto>> Handle(GetTarifasCuotaQuery request, CancellationToken cancellationToken)
    {
        var tarifas = await tarifaCuotaRepository.GetAllAsync(cancellationToken);

        return tarifas
            .Select(x => new TarifaCuotaDto(x.TipoAfiliacion, x.ValorMensualCOP))
            .ToList();
    }
}
