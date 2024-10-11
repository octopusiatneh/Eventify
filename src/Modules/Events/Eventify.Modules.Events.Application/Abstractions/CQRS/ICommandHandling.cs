using MediatR;

namespace Eventify.Modules.Events.Application.Abstractions.CQRS;

public interface ICommand : IRequest
{
}

public interface ICommandBus
{
    Task Send<TCommand>(TCommand command) where TCommand : ICommand;
}

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
{

}
