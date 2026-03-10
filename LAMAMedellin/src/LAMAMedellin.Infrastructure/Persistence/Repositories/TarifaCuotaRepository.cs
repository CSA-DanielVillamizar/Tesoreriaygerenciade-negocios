using LAMAMedellin.Application.Common.Interfaces.Repositories;
using LAMAMedellin.Domain.Entities;
using LAMAMedellin.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace LAMAMedellin.Infrastructure.Persistence.Repositories;

public sealed class TarifaCuotaRepository(LamaDbContext dbContext) : ITarifaCuotaRepository
{
    public Task<List<TarifaCuota>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.Set<TarifaCuota>()
            .OrderBy(x => x.TipoAfiliacion)
            .ToListAsync(cancellationToken);
    }

    public Task<TarifaCuota?> GetByTipoAfiliacionAsync(TipoAfiliacion tipoAfiliacion, CancellationToken cancellationToken = default)
    {
        return dbContext.Set<TarifaCuota>()
            .FirstOrDefaultAsync(x => x.TipoAfiliacion == tipoAfiliacion, cancellationToken);
    }

    public Task AddRangeAsync(IEnumerable<TarifaCuota> tarifas, CancellationToken cancellationToken = default)
    {
        return dbContext.Set<TarifaCuota>().AddRangeAsync(tarifas, cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
