using Eventify.Modules.Events.Application.Abstractions.CQRS;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Eventify.Modules.Events.Infrastructure.CQRS;

public sealed class CommandBus : ICommandBus
{
    private readonly IMediator _mediator;
    private readonly ILogger<CommandBus> _logger;

    public CommandBus(IMediator mediator, ILogger<CommandBus> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public Task Send<TCommand>(ICommand command)
    {
        _logger.LogInformation("Sending command: {Command}", command);

        return _mediator.Send(command);
    }


    public Task<TResponse> Send<TResponse>(ICommand<TResponse> command)
    {
        _logger.LogInformation("Sending command: {Command}", command);

        return _mediator.Send(command);
    }
}
