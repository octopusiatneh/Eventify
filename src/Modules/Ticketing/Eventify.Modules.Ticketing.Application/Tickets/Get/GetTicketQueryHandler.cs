using System.Data.Common;
using Dapper;
using Eventify.Modules.Ticketing.Domain.Tickets;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Application.Database;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Application.Tickets.Get;

internal sealed class GetTicketQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetTicketQuery, TicketResponse>
{
    public async Task<Result<TicketResponse>> Handle(GetTicketQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(TicketResponse.Id)},
                 customer_id AS {nameof(TicketResponse.CustomerId)},
                 order_id AS {nameof(TicketResponse.OrderId)},
                 event_id AS {nameof(TicketResponse.EventId)},
                 ticket_type_id AS {nameof(TicketResponse.TicketTypeId)},
                 code AS {nameof(TicketResponse.Code)},
                 created_at_utc AS {nameof(TicketResponse.CreatedAtUtc)}
             FROM ticketing.tickets
             WHERE id = @TicketId
             """;

        var ticket = await connection.QuerySingleOrDefaultAsync<TicketResponse>(sql, request);

        if (ticket is null)
        {
            return Result.Failure<TicketResponse>(TicketErrors.NotFound(request.TicketId));
        }

        return ticket;
    }
}
