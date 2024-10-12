namespace Eventify.Modules.Events.Domain.Categories;

public interface ICategoryRepository
{
    Task<Category> GetAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task InsertAsync(Category category, CancellationToken cancellationToken = default);
}
