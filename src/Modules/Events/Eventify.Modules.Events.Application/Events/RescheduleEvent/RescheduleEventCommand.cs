using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Events.Application.Events.RescheduleEvent;

public sealed record RescheduleEventCommand(DateTime? StartTime, DateTime? EndTime) : ICommand<Guid>;
