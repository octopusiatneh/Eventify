using Eventify.Modules.Events.Domain.Abstractions;
using MediatR;

namespace Eventify.Modules.Events.Application.Abstractions.CQRS;

public interface IBaseCommand;

public interface ICommand : IBaseCommand, IRequest<Result>;

public interface ICommand<TResponse> : IBaseCommand, IRequest<Result<TResponse>>;
