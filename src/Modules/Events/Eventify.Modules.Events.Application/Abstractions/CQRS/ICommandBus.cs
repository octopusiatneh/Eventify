namespace Eventify.Modules.Events.Application.Abstractions.CQRS;

public interface ICommandBus
{
    Task Send<TCommand>(ICommand command);

    Task<TResponse> Send<TResponse>(ICommand<TResponse> command);
}
