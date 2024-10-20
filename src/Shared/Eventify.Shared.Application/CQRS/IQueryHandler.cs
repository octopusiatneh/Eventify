using Eventify.Shared.Domain;
using MediatR;

namespace Eventify.Shared.Application.CQRS;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
