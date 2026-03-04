using LAMAMedellin.Application.Common.Interfaces.Repositories;
using MediatR;

namespace LAMAMedellin.Application.Features.Merchandising.Queries.GetArticuloById;

public sealed record GetArticuloByIdQuery(Guid Id) : IRequest<ArticuloDto?>;

public sealed record ArticuloDto(
    Guid Id,
    string Nombre,
    string SKU,
    string Descripcion,
    int CategoriaId,
    string Categoria,
    decimal PrecioVenta,
    decimal CostoPromedio,
    int StockActual,
    Guid CuentaContableIngresoId);

public sealed class GetArticuloByIdQueryHandler(IArticuloRepository articuloRepository)
    : IRequestHandler<GetArticuloByIdQuery, ArticuloDto?>
{
    public async Task<ArticuloDto?> Handle(GetArticuloByIdQuery request, CancellationToken cancellationToken)
    {
        var articulo = await articuloRepository.GetByIdAsync(request.Id, cancellationToken);
        if (articulo is null)
        {
            return null;
        }

        return new ArticuloDto(
            articulo.Id,
            articulo.Nombre,
            articulo.SKU,
            articulo.Descripcion,
            (int)articulo.Categoria,
            articulo.Categoria.ToString(),
            articulo.PrecioVenta,
            articulo.CostoPromedio,
            articulo.StockActual,
            articulo.CuentaContableIngresoId);
    }
}
