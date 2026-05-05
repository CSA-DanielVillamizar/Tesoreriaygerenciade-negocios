using LAMAMedellin.Domain.Entities;

namespace LAMAMedellin.Application.Common.Interfaces.Repositories;

public interface IUsuarioRepository
{
    Task<IReadOnlyList<Usuario>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Usuario?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Usuario?> GetByEntraObjectIdAsync(string entraObjectId, CancellationToken cancellationToken = default);
    Task AddAsync(Usuario usuario, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
