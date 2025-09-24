using Eventify.Modules.Ticketing.Domain.Events;
using Eventify.Modules.Ticketing.Domain.TicketTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventify.Modules.Ticketing.Infrastructure.TicketTypes;

internal sealed class TicketTypeConfiguration : IEntityTypeConfiguration<TicketType>
{
    public void Configure(EntityTypeBuilder<TicketType> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne<Event>().WithMany().HasForeignKey(t => t.EventId);
    }
}
