using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Events.Application.Events.GetEvent;

public sealed record GetEventQuery(Guid EventId) : IQuery<EventResponse>;
