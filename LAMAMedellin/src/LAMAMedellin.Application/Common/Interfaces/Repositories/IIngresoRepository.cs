using LAMAMedellin.Domain.Entities;

namespace LAMAMedellin.Application.Common.Interfaces.Repositories;

public interface IIngresoRepository
{
    Task<Ingreso?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Ingreso ingreso, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
