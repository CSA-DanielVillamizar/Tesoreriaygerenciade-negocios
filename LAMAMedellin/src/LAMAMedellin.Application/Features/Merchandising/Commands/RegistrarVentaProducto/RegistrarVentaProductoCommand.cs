using MediatR;

namespace LAMAMedellin.Application.Features.Merchandising.Commands.RegistrarVentaProducto;

public sealed record RegistrarVentaProductoCommand(
    Guid ProductoId,
    int Cantidad,
    Guid CajaId,
    string Concepto) : IRequest<Guid>;
