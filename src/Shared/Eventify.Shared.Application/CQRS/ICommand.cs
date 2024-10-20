using Eventify.Shared.Domain;
using MediatR;

namespace Eventify.Shared.Application.CQRS;

public interface IBaseCommand;

public interface ICommand : IBaseCommand, IRequest<Result>;

public interface ICommand<TResponse> : IBaseCommand, IRequest<Result<TResponse>>;
