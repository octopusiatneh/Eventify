﻿using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Events.Application.Events.Create;

public sealed record CreateEventCommand(
    Guid CategoryId,
    string Title,
    string Description,
    string Location,
    DateTime StartsAtUtc,
    DateTime? EndsAtUtc) : ICommand<Guid>;
