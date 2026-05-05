using MediatR;

namespace LAMAMedellin.Application.Features.Eventos.Queries.GetEventos;

public sealed record GetEventosQuery : IRequest<IReadOnlyList<EventoDto>>;
