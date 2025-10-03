using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Events.Application.Events.Reschedule;

public sealed record RescheduleEventCommand(Guid EventId, DateTime? StartsAtUtc, DateTime? EndsAtUtc) : ICommand;
