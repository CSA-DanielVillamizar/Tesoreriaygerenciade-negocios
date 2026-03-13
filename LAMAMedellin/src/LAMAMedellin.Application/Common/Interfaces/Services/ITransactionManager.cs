namespace LAMAMedellin.Application.Common.Interfaces.Services;

public interface ITransactionManager
{
    Task<TResult> ExecuteInTransactionAsync<TResult>(
        Func<CancellationToken, Task<TResult>> operation,
        CancellationToken cancellationToken = default);
}
