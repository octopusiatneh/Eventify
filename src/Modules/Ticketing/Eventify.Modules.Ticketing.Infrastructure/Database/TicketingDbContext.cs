using Eventify.Modules.Ticketing.Application.Abstractions.Data;
using Eventify.Modules.Ticketing.Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Modules.Ticketing.Infrastructure.Database;

public sealed class TicketingDbContext(DbContextOptions<TicketingDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Ticketing);
    }
}
