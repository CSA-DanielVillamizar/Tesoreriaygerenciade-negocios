using LAMAMedellin.Domain.Entities;

namespace LAMAMedellin.Application.Common.Interfaces.Repositories;

public interface ICajaRepository
{
    Task<IReadOnlyList<Caja>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Caja?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Caja caja, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
