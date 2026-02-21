using LAMAMedellin.Application.Common.Interfaces.Repositories;
using LAMAMedellin.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LAMAMedellin.Infrastructure.Persistence.Repositories;

public sealed class CuentaPorCobrarRepository(LamaDbContext dbContext) : ICuentaPorCobrarRepository
{
    public Task<bool> ExistePorMiembroYPeriodoAsync(Guid miembroId, string periodo, CancellationToken cancellationToken = default)
    {
        return dbContext.CuentasPorCobrar
            .AnyAsync(c => c.MiembroId == miembroId && c.Periodo == periodo, cancellationToken);
    }

    public Task AddRangeAsync(IEnumerable<CuentaPorCobrar> cuentasPorCobrar, CancellationToken cancellationToken = default)
    {
        return dbContext.CuentasPorCobrar
            .AddRangeAsync(cuentasPorCobrar, cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
