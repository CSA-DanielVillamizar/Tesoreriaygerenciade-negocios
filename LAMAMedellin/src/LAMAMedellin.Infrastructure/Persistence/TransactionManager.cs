using LAMAMedellin.Application.Common.Interfaces.Services;
using Microsoft.EntityFrameworkCore.Storage;

namespace LAMAMedellin.Infrastructure.Persistence;

public sealed class TransactionManager(LamaDbContext context) : ITransactionManager
{
    public async Task<TResult> ExecuteInTransactionAsync<TResult>(
        Func<CancellationToken, Task<TResult>> operation,
        CancellationToken cancellationToken = default)
    {
        await using IDbContextTransaction transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var result = await operation(cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return result;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
