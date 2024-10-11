using System.Data.Common;
using Dapper;
using MediatR;

namespace Eventify.Modules.Events.Application.Events;

public sealed record GetEventQuery(Guid EventId) : IRequest<EventResponse?>;

public sealed class GetEventHandler : IRequestHandler<GetEventQuery, EventResponse?>
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public GetEventHandler(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<EventResponse?> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection dbConnection = await _dbConnectionFactory.OpenConnectionAsync();
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
             LEFT JOIN events.ticket_types tt ON tt.event_id = e.id
             WHERE e.id = @EventId
             """;

        EventResponse? @event = await dbConnection.QuerySingleOrDefaultAsync(sql, request);

        return @event;
    }
}

public sealed record EventResponse(
    Guid Id,
    string Title,
    string Description,
    string Location,
    DateTime StartsAtUtc,
    DateTime? EndsAtUtc);
