namespace Eventify.Modules.Events.Application.Abstractions.Clock;

public interface IDateTimeProvider
{
    DateTime NowUtc { get; }
}
