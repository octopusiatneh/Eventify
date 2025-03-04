﻿using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Users.Application.Users.Get;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;
