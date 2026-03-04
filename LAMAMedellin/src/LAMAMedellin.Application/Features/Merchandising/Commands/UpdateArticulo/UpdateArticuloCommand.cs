using LAMAMedellin.Domain.Enums;
using MediatR;

namespace LAMAMedellin.Application.Features.Merchandising.Commands.UpdateArticulo;

public sealed record UpdateArticuloCommand(
    Guid Id,
    string Nombre,
    string SKU,
    string Descripcion,
    CategoriaArticulo Categoria,
    decimal PrecioVenta,
    decimal CostoPromedio,
    int StockActual,
    Guid CuentaContableIngresoId) : IRequest;
