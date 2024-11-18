using Eventify.Modules.Users.Presentation.Abstractions.Data;
using Eventify.Modules.Users.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Modules.Users.Infrastructure.Database;

public sealed class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);
    }
}
