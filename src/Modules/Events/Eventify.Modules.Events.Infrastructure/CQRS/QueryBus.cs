using Eventify.Modules.Events.Application.Abstractions.CQRS;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Eventify.Modules.Events.Infrastructure.CQRS;

public sealed class QueryBus : IQueryBus
{
    private readonly IMediator _mediator;
    private readonly ILogger<QueryBus> _logger;

    public QueryBus(IMediator mediator, ILogger<QueryBus> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public Task<TResponse> Send<TResponse>(IQuery<TResponse> query)
    {
        _logger.LogInformation("Executing query: {Query}", query);

        return _mediator.Send(query);
    }
}
