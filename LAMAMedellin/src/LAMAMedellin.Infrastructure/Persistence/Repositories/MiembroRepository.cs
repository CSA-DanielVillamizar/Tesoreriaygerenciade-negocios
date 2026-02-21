using LAMAMedellin.Application.Common.Interfaces.Repositories;
using LAMAMedellin.Domain.Entities;
using LAMAMedellin.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace LAMAMedellin.Infrastructure.Persistence.Repositories;

public sealed class MiembroRepository(LamaDbContext dbContext) : IMiembroRepository
{
    public async Task<IReadOnlyList<Miembro>> GetActivosAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Miembros
            .Where(m => m.Estado == EstadoMiembro.Activo)
            .ToListAsync(cancellationToken);
    }
}
