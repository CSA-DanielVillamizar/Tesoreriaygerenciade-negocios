using MediatR;

namespace LAMAMedellin.Application.Features.Eventos.Queries.GetEventoById;

public sealed record GetEventoByIdQuery(Guid Id) : IRequest<EventoDetalleDto?>;
