using Eventify.Modules.Users.Application.Abstractions;
using Eventify.Modules.Users.Domain.Users;
using Eventify.Modules.Users.Infrastructure.Users.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Modules.Users.Infrastructure.Database;

public sealed class UsersDbContext(DbContextOptions<UsersDbContext> options)
    : DbContext(options), IUnitOfWork
{
    internal DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
    }
}
