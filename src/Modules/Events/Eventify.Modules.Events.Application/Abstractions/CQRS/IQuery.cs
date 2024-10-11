using MediatR;

namespace Eventify.Modules.Events.Application.Abstractions.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
