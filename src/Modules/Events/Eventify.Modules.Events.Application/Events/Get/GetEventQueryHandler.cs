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
                 e.category_id AS {nameof(EventResponse.CategoryId)},
                 e.title AS {nameof(EventResponse.Title)},
                 e.description AS {nameof(EventResponse.Description)},
                 e.location AS {nameof(EventResponse.Location)},
                 e.starts_at_utc AS {nameof(EventResponse.StartsAtUtc)},
                 e.ends_at_utc AS {nameof(EventResponse.EndsAtUtc)},
                 tt.id AS {nameof(TicketTypeResponse.TicketTypeId)},
                 tt.name AS {nameof(TicketTypeResponse.Name)},
                 tt.price AS {nameof(TicketTypeResponse.Price)},
                 tt.currency AS {nameof(TicketTypeResponse.Currency)},
                 tt.quantity AS {nameof(TicketTypeResponse.Quantity)}
             FROM events.events e
             LEFT JOIN events.ticket_types tt ON tt.event_id = e.id
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
