using Eventify.Modules.Events.Domain.Categories;
using Eventify.Modules.Events.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Modules.Events.Infrastructure.Events;

internal sealed class CategoryRepository : ICategoryRepository
{
    private readonly EventsDbContext _dbContext;

    public CategoryRepository(EventsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Category> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Categories.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task InsertAsync(Category category, CancellationToken cancellationToken = default)
    {
        await _dbContext.Categories.AddAsync(category, cancellationToken);
    }
}
