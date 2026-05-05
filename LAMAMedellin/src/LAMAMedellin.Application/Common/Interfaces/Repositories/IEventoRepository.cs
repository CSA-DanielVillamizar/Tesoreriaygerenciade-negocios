using LAMAMedellin.Domain.Entities;

namespace LAMAMedellin.Application.Common.Interfaces.Repositories;

public interface IEventoRepository
{
    Task<IReadOnlyList<Evento>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Evento?> GetProximoProgramadoAsync(CancellationToken cancellationToken = default);
    Task<Evento?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Evento?> GetByIdWithAsistenciasAsync(Guid id, CancellationToken cancellationToken = default);
    Task<AsistenciaEvento?> GetAsistenciaAsync(Guid eventoId, Guid miembroId, CancellationToken cancellationToken = default);
    Task AddAsync(Evento evento, CancellationToken cancellationToken = default);
    Task AddAsistenciaAsync(AsistenciaEvento asistenciaEvento, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
