using MediatR;

namespace LAMAMedellin.Application.Features.Reportes.Queries.GetEstadoResultados;

public sealed record GetEstadoResultadosQuery(
    DateTime FechaInicio,
    DateTime FechaFin) : IRequest<EstadoResultadosDto>;
