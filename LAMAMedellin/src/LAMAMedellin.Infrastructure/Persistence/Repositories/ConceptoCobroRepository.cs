using LAMAMedellin.Application.Common.Interfaces.Repositories;
using LAMAMedellin.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LAMAMedellin.Infrastructure.Persistence.Repositories;

public sealed class ConceptoCobroRepository(LamaDbContext dbContext) : IConceptoCobroRepository
{
    public Task<ConceptoCobro?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return dbContext.ConceptosCobro.FirstOrDefaultAsync(
            concepto => concepto.Id == id,
            cancellationToken);
    }

    public Task<List<ConceptoCobro>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.ConceptosCobro
            .OrderBy(concepto => concepto.Nombre)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ConceptoCobro conceptoCobro, CancellationToken cancellationToken = default)
    {
        await dbContext.ConceptosCobro.AddAsync(conceptoCobro, cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
