using LAMAMedellin.Application.Common.Interfaces.Repositories;
using LAMAMedellin.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LAMAMedellin.Infrastructure.Persistence.Repositories;

public sealed class CajaRepository(LamaDbContext context) : ICajaRepository
{
    public async Task<IReadOnlyList<Caja>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Cajas
            .Include(x => x.CuentaContable)
            .AsNoTracking()
            .OrderBy(x => x.Nombre)
            .ToListAsync(cancellationToken);
    }

    public Task<Caja?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.Cajas
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<decimal> GetTotalSaldoActualAsync(CancellationToken cancellationToken = default)
    {
        return await context.Cajas
            .Select(x => (decimal?)x.SaldoActual)
            .SumAsync(cancellationToken) ?? 0m;
    }

    public async Task AddAsync(Caja caja, CancellationToken cancellationToken = default)
    {
        await context.Cajas.AddAsync(caja, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}
