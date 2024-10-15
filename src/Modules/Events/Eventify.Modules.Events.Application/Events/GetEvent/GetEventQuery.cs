using Eventify.Modules.Events.Application.Abstractions.CQRS;

namespace Eventify.Modules.Events.Application.Events.GetEvent;

public sealed record GetEventQuery(Guid EventId) : IQuery<EventResponse>;
