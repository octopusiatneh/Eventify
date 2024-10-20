using Eventify.Shared.Application.Clock;
using Eventify.Shared.Application.Database;
using Eventify.Shared.Infrastructure.Clock;
using Eventify.Shared.Infrastructure.Data;
using Eventify.Shared.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;

namespace Eventify.Shared.Infrastructure;

public static class SharedInfrastructureConfiguration
{
    public static IServiceCollection AddSharedInfrastructureConfig(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<DbConnectionStringOptions>(
            opts => configuration.GetSection(DbConnectionStringOptions.DbConnectionString)
        );
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        var dbConnectionString = configuration.GetConnectionString("Database")!;
        var npgsqlDataSource = new NpgsqlDataSourceBuilder(dbConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);

        return services;
    }
}
