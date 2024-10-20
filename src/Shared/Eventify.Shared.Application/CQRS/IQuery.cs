using Eventify.Shared.Domain;
using MediatR;

namespace Eventify.Shared.Application.CQRS;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;

