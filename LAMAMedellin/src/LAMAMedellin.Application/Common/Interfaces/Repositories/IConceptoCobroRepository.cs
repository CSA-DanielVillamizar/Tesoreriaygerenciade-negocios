using LAMAMedellin.Domain.Entities;

namespace LAMAMedellin.Application.Common.Interfaces.Repositories;

public interface IConceptoCobroRepository
{
    Task<ConceptoCobro?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<ConceptoCobro>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(ConceptoCobro conceptoCobro, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
