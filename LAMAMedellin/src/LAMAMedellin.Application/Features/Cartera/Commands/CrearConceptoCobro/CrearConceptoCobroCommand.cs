using MediatR;

namespace LAMAMedellin.Application.Features.Cartera.Commands.CrearConceptoCobro;

public sealed record CrearConceptoCobroCommand(
    string Nombre,
    decimal ValorCOP,
    int PeriodicidadMensual,
    Guid CuentaContableIngresoId) : IRequest<Guid>;
