using LAMAMedellin.Domain.Enums;

namespace LAMAMedellin.Application.Features.Tesoreria.Queries.GetCajas;

public sealed record CajaDto(
    Guid Id,
    string Nombre,
    TipoCaja TipoCaja,
    decimal SaldoActual);
