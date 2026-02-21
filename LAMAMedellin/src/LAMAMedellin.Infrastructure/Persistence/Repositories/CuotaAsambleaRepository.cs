using LAMAMedellin.Application.Common.Interfaces.Repositories;
using LAMAMedellin.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LAMAMedellin.Infrastructure.Persistence.Repositories;

public sealed class CuotaAsambleaRepository(LamaDbContext dbContext) : ICuotaAsambleaRepository
{
    public Task<CuotaAsamblea?> GetByAnioAsync(int anio, CancellationToken cancellationToken = default)
    {
        return dbContext.CuotasAsamblea
            .FirstOrDefaultAsync(c => c.Anio == anio, cancellationToken);
    }
}
