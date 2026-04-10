using MediatR;

namespace LAMAMedellin.Application.Features.Cartera.Queries.GetMiembrosLookup;

public sealed record GetMiembrosLookupQuery : IRequest<List<MiembroLookupDto>>;
