using LAMAMedellin.Domain.Enums;
using MediatR;

namespace LAMAMedellin.Application.Features.Miembros.Commands.UpdateMiembro;

public sealed record UpdateMiembroCommand(
    Guid Id,
    string MarcaMoto,
    string ModeloMoto,
    int Cilindraje,
    string Placa,
    RangoClub Rango,
    bool EsActivo) : IRequest<Unit>;
