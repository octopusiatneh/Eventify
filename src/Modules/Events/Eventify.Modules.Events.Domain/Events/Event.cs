using Eventify.Modules.Events.Domain.Categories;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Events.Domain.Events;

public sealed class Event : Entity
{
    private Event()
    {
    }

    public Guid Id { get; private set; }

    public Guid CategoryId { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public string Location { get; private set; }

    public DateTime StartsAtUtc { get; private set; }

    public DateTime? EndsAtUtc { get; private set; }

    public EventStatus Status { get; private set; }

    public static Event Create(Category category,
                               string title,
                               string description,
                               string location,
                               DateTime startsAtUtc,
                               DateTime? endsAtUtc)
    {
        var @event = new Event
        {
            Id = Guid.NewGuid(),
            CategoryId = category.Id,
            Title = title,
            Description = description,
            Location = location,
            StartsAtUtc = startsAtUtc,
            EndsAtUtc = endsAtUtc,
            Status = EventStatus.Draft
        };
        
        @event.Raise(new EventCreatedEvent(@event.Id));

        return @event;
    }
}
