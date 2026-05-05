using LAMAMedellin.Application.Common.Interfaces.Repositories;
using LAMAMedellin.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LAMAMedellin.Infrastructure.Persistence.Repositories;

public sealed class UsuarioRepository(LamaDbContext dbContext) : IUsuarioRepository
{
    public async Task<IReadOnlyList<Usuario>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Usuarios
            .OrderBy(u => u.Email)
            .ToListAsync(cancellationToken);
    }

    public Task<Usuario?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public Task<Usuario?> GetByEntraObjectIdAsync(string entraObjectId, CancellationToken cancellationToken = default)
    {
        return dbContext.Usuarios
            .FirstOrDefaultAsync(u => u.EntraObjectId == entraObjectId, cancellationToken);
    }

    public Task AddAsync(Usuario usuario, CancellationToken cancellationToken = default)
    {
        return dbContext.Usuarios.AddAsync(usuario, cancellationToken).AsTask();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
