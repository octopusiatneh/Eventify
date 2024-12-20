﻿namespace Eventify.Shared.Infrastructure.Options;

public class DbConnectionStringOptions
{
    public const string DbConnectionString = "ConnectionStrings";

    public string Database { get; init; } = string.Empty;
}
