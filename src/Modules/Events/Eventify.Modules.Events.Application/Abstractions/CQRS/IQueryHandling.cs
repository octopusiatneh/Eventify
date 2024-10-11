using MediatR;

namespace Eventify.Modules.Events.Application.Abstractions.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}

public interface IQueryBus
{
    Task<TResponse> Send<TResponse>(IQuery<TResponse> query);
}

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}
