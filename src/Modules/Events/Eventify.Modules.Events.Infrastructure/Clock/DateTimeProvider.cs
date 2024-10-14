using Eventify.Modules.Events.Application.Abstractions.Clock;

namespace Eventify.Modules.Events.Infrastructure.Clock;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime NowUtc => DateTime.UtcNow;
}
