using MediatR;

namespace LAMAMedellin.Application.Features.Reportes.Queries.GetCarteraMora;

public sealed record GetCarteraMoraQuery : IRequest<CarteraMoraDto>;
