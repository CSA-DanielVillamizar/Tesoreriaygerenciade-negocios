using LAMAMedellin.Application.Common.Exceptions;
using LAMAMedellin.Application.Common.Interfaces.Repositories;
using MediatR;

namespace LAMAMedellin.Application.Features.Merchandising.Commands.UpdateArticulo;

public sealed class UpdateArticuloCommandHandler(IArticuloRepository articuloRepository)
    : IRequestHandler<UpdateArticuloCommand>
{
    public async Task Handle(UpdateArticuloCommand request, CancellationToken cancellationToken)
    {
        var articulo = await articuloRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new ExcepcionNegocio("Artículo no encontrado.");

        var skuNormalizado = request.SKU.Trim();
        var articuloConMismoSku = await articuloRepository.GetBySkuAsync(skuNormalizado, cancellationToken);
        if (articuloConMismoSku is not null && articuloConMismoSku.Id != request.Id)
        {
            throw new ExcepcionNegocio("Ya existe un artículo con el mismo SKU.");
        }

        articulo.Actualizar(
            request.Nombre,
            skuNormalizado,
            request.Descripcion,
            request.Categoria,
            request.PrecioVenta,
            request.CostoPromedio,
            request.StockActual,
            request.CuentaContableIngresoId);

        await articuloRepository.SaveChangesAsync(cancellationToken);
    }
}
