﻿using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Domain.Events;

public sealed class EventRescheduled(Guid eventId, DateTime startsAtUtc, DateTime? endsAtUtc)
    : DomainEvent
{
    public Guid EventId { get; } = eventId;

    public DateTime StartsAtUtc { get; } = startsAtUtc;

    public DateTime? EndsAtUtc { get; } = endsAtUtc;
}
