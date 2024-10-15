﻿namespace Eventify.Modules.Events.Domain.Abstractions;

public enum ErrorType
{
    None = 0,
    Failure = 1,
    Validation = 2,
    Problem = 3,
    NotFound = 4,
    Conflict = 5
}
