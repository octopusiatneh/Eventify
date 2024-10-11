﻿using System.Data.Common;
using Eventify.Modules.Events.Application.Events;
using Npgsql;

namespace Eventify.Modules.Events.Infrastructure.Data;

public sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        return await dataSource.OpenConnectionAsync();
    }
}
