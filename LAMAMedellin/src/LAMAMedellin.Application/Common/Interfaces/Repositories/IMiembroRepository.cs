using LAMAMedellin.Domain.Entities;

namespace LAMAMedellin.Application.Common.Interfaces.Repositories;

public interface IMiembroRepository
{
    Task<IReadOnlyList<Miembro>> GetActivosAsync(CancellationToken cancellationToken = default);
}
