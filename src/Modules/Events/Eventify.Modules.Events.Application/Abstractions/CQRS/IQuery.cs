﻿using Eventify.Modules.Events.Domain.Abstractions;
using MediatR;

namespace Eventify.Modules.Events.Application.Abstractions.CQRS;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;

