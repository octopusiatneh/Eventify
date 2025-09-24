using Dapper;
using Eventify.Modules.Events.Domain.Events;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Application.Database;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Events.Application.Events.Get;

internal sealed class GetEventQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetEventQuery, EventResponse>
{
    public async Task<Result<EventResponse>> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        await using var dbConnection = await dbConnectionFactory.OpenConnectionAsync();
        const string sql =
            $"""
             SELECT
                 e.id AS {nameof(EventResponse.Id)},
                 e.title AS {nameof(EventResponse.Title)},
                 e.description AS {nameof(EventResponse.Description)},
                 e.location AS {nameof(EventResponse.Location)},
                 e.starts_at_utc AS {nameof(EventResponse.StartsAtUtc)},
                 e.ends_at_utc AS {nameof(EventResponse.EndsAtUtc)}
             FROM events.events e
             WHERE e.id = @EventId
             """;

        var @event = await dbConnection.QuerySingleOrDefaultAsync<EventResponse>(sql, request);

        if (@event is null)
        {
            return Result.Failure<EventResponse>(EventErrors.NotFound(request.EventId));
        }

        return @event!;
    }
}
