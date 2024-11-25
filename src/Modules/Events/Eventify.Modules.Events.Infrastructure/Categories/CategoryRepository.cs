using Eventify.Modules.Events.Domain.Categories;
using Eventify.Modules.Events.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Modules.Events.Infrastructure.Categories;

internal sealed class CategoryRepository(EventsDbContext dbContext) : ICategoryRepository
{
    public async Task<Category?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await dbContext.Categories.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task InsertAsync(Category category, CancellationToken cancellationToken = default)
        => await dbContext.Categories.AddAsync(category, cancellationToken);
}
