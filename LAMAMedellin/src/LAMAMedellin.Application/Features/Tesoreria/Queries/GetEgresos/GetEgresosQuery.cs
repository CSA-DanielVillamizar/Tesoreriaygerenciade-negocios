using MediatR;

namespace LAMAMedellin.Application.Features.Tesoreria.Queries.GetEgresos;

public sealed record GetEgresosQuery : IRequest<List<EgresoDto>>;
