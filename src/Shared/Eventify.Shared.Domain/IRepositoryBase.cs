namespace Eventify.Shared.Domain;

public interface IRepositoryBase<T>
{
    Task<T?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task InsertAsync(T entity, CancellationToken cancellationToken = default);
}
