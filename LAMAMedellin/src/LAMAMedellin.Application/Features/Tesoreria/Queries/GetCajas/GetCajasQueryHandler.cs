using LAMAMedellin.Application.Common.Interfaces.Repositories;
using MediatR;

namespace LAMAMedellin.Application.Features.Tesoreria.Queries.GetCajas;

public sealed class GetCajasQueryHandler(ICajaRepository cajaRepository)
    : IRequestHandler<GetCajasQuery, List<CajaDto>>
{
    public async Task<List<CajaDto>> Handle(GetCajasQuery request, CancellationToken cancellationToken)
    {
        var cajas = await cajaRepository.GetAllAsync(cancellationToken);

        return cajas
            .Select(caja => new CajaDto(
                caja.Id,
                caja.Nombre,
                caja.TipoCaja,
                caja.SaldoActual))
            .ToList();
    }
}
