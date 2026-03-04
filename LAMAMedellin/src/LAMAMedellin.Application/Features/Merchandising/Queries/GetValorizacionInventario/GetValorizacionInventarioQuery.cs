using LAMAMedellin.Application.Common.Interfaces.Repositories;
using MediatR;

namespace LAMAMedellin.Application.Features.Merchandising.Queries.GetValorizacionInventario;

public sealed record GetValorizacionInventarioQuery : IRequest<ValorizacionInventarioDto>;

public sealed record ValorizacionInventarioDto(
    int TotalArticulosActivos,
    int StockTotal,
    decimal ValorTotalInventario);

public sealed class GetValorizacionInventarioQueryHandler(IArticuloRepository articuloRepository)
    : IRequestHandler<GetValorizacionInventarioQuery, ValorizacionInventarioDto>
{
    public async Task<ValorizacionInventarioDto> Handle(GetValorizacionInventarioQuery request, CancellationToken cancellationToken)
    {
        var articulos = await articuloRepository.GetAllAsync(cancellationToken);

        var totalArticulosActivos = articulos.Count;
        var stockTotal = articulos.Sum(x => x.StockActual);
        var valorTotalInventario = articulos.Sum(x => x.CostoPromedio * x.StockActual);

        return new ValorizacionInventarioDto(
            totalArticulosActivos,
            stockTotal,
            valorTotalInventario);
    }
}
