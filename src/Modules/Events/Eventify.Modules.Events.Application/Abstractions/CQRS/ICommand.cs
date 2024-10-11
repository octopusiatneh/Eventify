using MediatR;

namespace Eventify.Modules.Events.Application.Abstractions.CQRS;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
