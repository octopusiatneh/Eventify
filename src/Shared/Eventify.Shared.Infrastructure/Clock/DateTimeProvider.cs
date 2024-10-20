using Eventify.Shared.Application.Clock;

namespace Eventify.Shared.Infrastructure.Clock;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime NowUtc => DateTime.UtcNow;
}
