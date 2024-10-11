namespace Eventify.Modules.Events.Application.Abstractions.CQRS;

public interface IQueryBus
{
    Task<TResponse> Send<TResponse>(IQuery<TResponse> query);
}
