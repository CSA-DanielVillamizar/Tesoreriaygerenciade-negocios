using LAMAMedellin.Application.Common.Interfaces.Repositories;
using LAMAMedellin.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LAMAMedellin.Infrastructure.Persistence.Repositories;

public sealed class IngresoRepository(LamaDbContext context) : IIngresoRepository
{
    public Task<Ingreso?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Ingresos
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(Ingreso ingreso, CancellationToken cancellationToken = default)
    {
        await context.Ingresos.AddAsync(ingreso, cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}
