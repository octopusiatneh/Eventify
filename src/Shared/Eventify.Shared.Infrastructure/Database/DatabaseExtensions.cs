using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;

namespace Eventify.Shared.Infrastructure.Database;

internal static class DatabaseExtensions
{
    internal static void AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnectionString = configuration.GetConnectionString("Database")!;
        var npgsqlDataSource = new NpgsqlDataSourceBuilder(dbConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);
    }
}
