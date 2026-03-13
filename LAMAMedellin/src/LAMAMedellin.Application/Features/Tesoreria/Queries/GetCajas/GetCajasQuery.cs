using MediatR;

namespace LAMAMedellin.Application.Features.Tesoreria.Queries.GetCajas;

public sealed record GetCajasQuery : IRequest<List<CajaDto>>;
