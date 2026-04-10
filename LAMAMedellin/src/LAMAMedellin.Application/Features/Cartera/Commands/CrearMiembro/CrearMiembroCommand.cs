using LAMAMedellin.Domain.Enums;
using MediatR;

namespace LAMAMedellin.Application.Features.Cartera.Commands.CrearMiembro;

public sealed record CrearMiembroCommand(
    string DocumentoIdentidad,
    string Nombres,
    string Apellidos,
    string Apodo,
    DateOnly FechaIngreso,
    TipoMiembro TipoMiembro) : IRequest<Guid>;
