using LAMAMedellin.Domain.Entities;

namespace LAMAMedellin.Application.Common.Interfaces.Repositories;

public interface IMiembroRepository
{
    Task<IReadOnlyList<Miembro>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Miembro>> GetActivosAsync(CancellationToken cancellationToken = default);
}
