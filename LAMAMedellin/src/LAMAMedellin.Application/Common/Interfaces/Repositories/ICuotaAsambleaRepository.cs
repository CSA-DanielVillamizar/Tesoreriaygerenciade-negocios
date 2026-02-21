using LAMAMedellin.Domain.Entities;

namespace LAMAMedellin.Application.Common.Interfaces.Repositories;

public interface ICuotaAsambleaRepository
{
    Task<CuotaAsamblea?> GetByAnioAsync(int anio, CancellationToken cancellationToken = default);
}
