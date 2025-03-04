﻿using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Users.Application.Users.Register;

public sealed record RegisterUserCommand(string Email, string Password, string FirstName, string LastName) : ICommand<Guid>;
