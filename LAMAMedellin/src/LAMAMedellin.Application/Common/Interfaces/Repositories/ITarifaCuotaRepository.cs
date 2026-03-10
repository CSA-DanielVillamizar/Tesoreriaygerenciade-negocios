using LAMAMedellin.Domain.Entities;
using LAMAMedellin.Domain.Enums;

namespace LAMAMedellin.Application.Common.Interfaces.Repositories;

public interface ITarifaCuotaRepository
{
    Task<List<TarifaCuota>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TarifaCuota?> GetByTipoAfiliacionAsync(TipoAfiliacion tipoAfiliacion, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<TarifaCuota> tarifas, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
