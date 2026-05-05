using LAMAMedellin.Application.Common.Interfaces.Repositories;
using LAMAMedellin.Domain.Entities;
using LAMAMedellin.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace LAMAMedellin.Infrastructure.Persistence.Repositories;

public sealed class EventoRepository(LamaDbContext dbContext) : IEventoRepository
{
    public async Task<IReadOnlyList<Evento>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Eventos
            .OrderBy(e => e.FechaProgramada)
            .ToListAsync(cancellationToken);
    }

    public Task<Evento?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return dbContext.Eventos.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<Evento?> GetProximoProgramadoAsync(CancellationToken cancellationToken = default)
    {
        var nowUtc = DateTime.UtcNow;

        var proximoFuturo = await dbContext.Eventos
            .Where(e => e.Estado == EstadoEvento.Programado && e.FechaProgramada >= nowUtc)
            .OrderBy(e => e.FechaProgramada)
            .FirstOrDefaultAsync(cancellationToken);

        if (proximoFuturo is not null)
        {
            return proximoFuturo;
        }

        return await dbContext.Eventos
            .Where(e => e.Estado == EstadoEvento.Programado)
            .OrderBy(e => e.FechaProgramada)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<Evento?> GetByIdWithAsistenciasAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return dbContext.Eventos
            .Include(e => e.Asistencias)
            .ThenInclude(a => a.Miembro)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public Task<AsistenciaEvento?> GetAsistenciaAsync(Guid eventoId, Guid miembroId, CancellationToken cancellationToken = default)
    {
        return dbContext.AsistenciasEvento
            .FirstOrDefaultAsync(a => a.EventoId == eventoId && a.MiembroId == miembroId, cancellationToken);
    }

    public Task AddAsync(Evento evento, CancellationToken cancellationToken = default)
    {
        return dbContext.Eventos.AddAsync(evento, cancellationToken).AsTask();
    }

    public Task AddAsistenciaAsync(AsistenciaEvento asistenciaEvento, CancellationToken cancellationToken = default)
    {
        return dbContext.AsistenciasEvento.AddAsync(asistenciaEvento, cancellationToken).AsTask();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
