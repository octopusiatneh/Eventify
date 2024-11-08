using Eventify.Modules.Users.Application.Abstractions.Data;
using Eventify.Modules.Users.Domain.Users;
using Eventify.Modules.Users.Infrastructure.Database;
using Eventify.Modules.Users.Infrastructure.Users;
using Eventify.Modules.Users.Presentation.Users;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Modules.Users.Infrastructure;

public static class UsersModule
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        UserEndpoints.MapEndpoints(app);
    }

    public static IServiceCollection AddUsersModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseConnectionString = configuration.GetConnectionString("Database")!;

        services.AddDbContext<UsersDbContext>(options =>
            options
                .UseNpgsql(
                    databaseConnectionString,
                    npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users)
                )
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UsersDbContext>());
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
