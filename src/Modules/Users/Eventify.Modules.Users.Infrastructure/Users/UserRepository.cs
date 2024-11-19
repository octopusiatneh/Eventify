using Eventify.Modules.Users.Domain.Users;
using Eventify.Modules.Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Modules.Users.Infrastructure.Users;

internal sealed class UserRepository(UsersDbContext dbContext) : IUserRepository
{
    public async Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await dbContext.Users.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task InsertAsync(User user, CancellationToken cancellationToken = default)
        => await dbContext.Users.AddAsync(user, cancellationToken);
}
