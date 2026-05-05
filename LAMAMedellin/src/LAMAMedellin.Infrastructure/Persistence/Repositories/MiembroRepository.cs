using LAMAMedellin.Application.Common.Interfaces.Repositories;
using LAMAMedellin.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LAMAMedellin.Infrastructure.Persistence.Repositories;

public sealed class MiembroRepository(LamaDbContext dbContext) : IMiembroRepository
{
    public async Task<IReadOnlyList<Miembro>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Miembros
            .OrderBy(m => m.Nombres)
            .ThenBy(m => m.Apellidos)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Miembro>> GetActivosAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Miembros
            .Where(m => m.EsActivo)
            .ToListAsync(cancellationToken);
    }

    public Task<Miembro?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return dbContext.Miembros.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public Task<Miembro?> GetByDocumentoAsync(string documento, CancellationToken cancellationToken = default)
    {
        return dbContext.Miembros.FirstOrDefaultAsync(m => m.DocumentoIdentidad == documento, cancellationToken);
    }

    public Task AddAsync(Miembro miembro, CancellationToken cancellationToken = default)
    {
        return dbContext.Miembros.AddAsync(miembro, cancellationToken).AsTask();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
