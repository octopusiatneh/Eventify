using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Events.Application.Events.PublishEvent;

public sealed record PublishEventCommand(Guid EventId) : ICommand;
