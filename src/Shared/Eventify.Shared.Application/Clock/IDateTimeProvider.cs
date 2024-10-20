namespace Eventify.Shared.Application.Clock;

public interface IDateTimeProvider
{
    DateTime NowUtc { get; }
}
