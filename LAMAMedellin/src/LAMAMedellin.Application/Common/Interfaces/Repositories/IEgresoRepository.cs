using LAMAMedellin.Domain.Entities;

namespace LAMAMedellin.Application.Common.Interfaces.Repositories;

public interface IEgresoRepository
{
    Task<IReadOnlyList<Egreso>> GetAllWithDetallesAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Egreso>> GetByFechaRangoAsync(
        DateTime fechaInicio,
        DateTime fechaFin,
        CancellationToken cancellationToken = default);
    Task AddAsync(Egreso egreso, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
