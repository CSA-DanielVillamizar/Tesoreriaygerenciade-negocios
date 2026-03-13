using LAMAMedellin.Domain.Entities;

namespace LAMAMedellin.Application.Common.Interfaces.Repositories;

public interface ICuentaContableRepository
{
    Task<IReadOnlyList<CuentaContable>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CuentaContable>> GetByIdsAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken = default);
    Task<CuentaContable?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<CuentaContable?> GetByCodigoAsync(string codigo, CancellationToken cancellationToken = default);
}
