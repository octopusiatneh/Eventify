using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Events.Application.Events.Reschedule;

public sealed record RescheduleEventCommand(DateTime? StartTime, DateTime? EndTime) : ICommand<Guid>;
