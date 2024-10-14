namespace Eventify.Modules.Events.Domain.Abstractions;

public interface IRepositoryBase<T>
{
    Task<T?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task InsertAsync(T entity, CancellationToken cancellationToken = default);
}
