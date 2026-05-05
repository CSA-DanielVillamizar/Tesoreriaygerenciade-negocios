using MediatR;

namespace LAMAMedellin.Application.Features.Eventos.Commands.MarcarAsistencia;

public sealed record MarcarAsistenciaCommand(
    Guid EventoId,
    Guid MiembroId,
    bool Asistio,
    string? Observaciones) : IRequest<Unit>;
