using Eventify.Modules.Events.Domain.Events;
using Eventify.Modules.Events.Domain.TicketTypes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Modules.Events.Infrastructure.TicketTypes;

internal sealed class TicketTypeConfiguration : IEntityTypeConfiguration<TicketType>
{
    public void Configure(EntityTypeBuilder<TicketType> builder)
    {
        builder.HasOne<Event>().WithMany().HasForeignKey(t => t.EventId);
    }
}
