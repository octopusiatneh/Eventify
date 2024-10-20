using System.Data.Common;
using Eventify.Shared.Application.Database;
using Npgsql;

namespace Eventify.Shared.Infrastructure.Data;

public sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        return await dataSource.OpenConnectionAsync();
    }
}
