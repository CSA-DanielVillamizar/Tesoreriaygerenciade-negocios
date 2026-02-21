using LAMAMedellin.Domain.Entities;

namespace LAMAMedellin.Application.Common.Interfaces.Repositories;

public interface ICuentaPorCobrarRepository
{
    Task<bool> ExistePorMiembroYPeriodoAsync(Guid miembroId, string periodo, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<CuentaPorCobrar> cuentasPorCobrar, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
