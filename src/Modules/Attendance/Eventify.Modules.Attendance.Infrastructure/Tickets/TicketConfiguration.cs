using Eventify.Modules.Attendance.Domain.Attendees;
using Eventify.Modules.Attendance.Domain.Events;
using Eventify.Modules.Attendance.Domain.Tickets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventify.Modules.Attendance.Infrastructure.Tickets;

internal sealed class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(t => t.Id);

        builder
            .Property(t => t.Code)
            .IsRequired()
            .HasMaxLength(30);

        builder
            .Property(t => t.AttendeeId)
            .IsRequired();

        builder
            .Property(t => t.EventId)
            .IsRequired();

        builder
            .HasIndex(t => new { t.EventId, t.Code })
            .IsUnique();

        builder
            .HasIndex(t => t.AttendeeId);

        builder
            .HasIndex(t => t.UsedAtUtc);

        builder
            .HasOne<Attendee>()
            .WithMany()
            .HasForeignKey(t => t.AttendeeId);

        builder
            .HasOne<Event>()
            .WithMany()
            .HasForeignKey(t => t.EventId);
    }
}
