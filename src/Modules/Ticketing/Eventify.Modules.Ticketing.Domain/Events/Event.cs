﻿using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Domain.Events;

public sealed class Event : Entity
{
    private Event()
    {
    }

    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public string Location { get; private set; }

    public DateTime StartsAtUtc { get; private set; }

    public DateTime? EndsAtUtc { get; private set; }

    public bool Canceled { get; private set; }

    public static Event Create(
        Guid id,
        string title,
        string description,
        string location,
        DateTime startsAtUtc,
        DateTime? endsAtUtc)
    {
        var @event = new Event
        {
            Id = id,
            Title = title,
            Description = description,
            Location = location,
            StartsAtUtc = startsAtUtc,
            EndsAtUtc = endsAtUtc
        };

        return @event;
    }

    public void Reschedule(DateTime startsAtUtc, DateTime? endsAtUtc)
    {
        StartsAtUtc = startsAtUtc;
        EndsAtUtc = endsAtUtc;

        Raise(new EventRescheduled(Id, StartsAtUtc, EndsAtUtc));
    }

    public void Cancel()
    {
        if (Canceled)
        {
            return;
        }

        Canceled = true;

        Raise(new EventCanceled(Id));
    }

    public void PaymentsRefunded()
    {
        Raise(new EventPaymentsRefunded(Id));
    }

    public void TicketsArchived()
    {
        Raise(new EventTicketsArchived(Id));
    }
}
