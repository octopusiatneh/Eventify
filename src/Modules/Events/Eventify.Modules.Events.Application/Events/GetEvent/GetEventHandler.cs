using Dapper;
using Eventify.Modules.Events.Application.Abstractions.CQRS;
using Eventify.Modules.Events.Application.Abstractions.Database;
using Eventify.Modules.Events.Domain.Abstractions;

namespace Eventify.Modules.Events.Application.Events.GetEvent;

public sealed class GetEventHandler : IQueryHandler<GetEventQuery, EventResponse>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GetEventHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Result<EventResponse>> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        await using var dbConnection = await _dbConnectionFactory.OpenConnectionAsync();
        const string sql =
            $"""
             SELECT
                 e.id AS {nameof(EventResponse.Id)},
                 e.title AS {nameof(EventResponse.Title)},
                 e.description AS {nameof(EventResponse.Description)},
                 e.location AS {nameof(EventResponse.Location)},
                 e.starts_at_utc AS {nameof(EventResponse.StartsAtUtc)},
                 e.ends_at_utc AS {nameof(EventResponse.EndsAtUtc)},
             FROM events.events e
             WHERE e.id = @EventId
             """;

        EventResponse? @event = await dbConnection.QuerySingleOrDefaultAsync(sql, request);

        return @event;
    }
}
