using Eventify.Modules.Users.Application.Abstractions;
using Eventify.Modules.Users.Application.Abstractions.Identity;
using Eventify.Modules.Users.Domain.Users;
using Eventify.Modules.Users.Infrastructure.Authorization;
using Eventify.Modules.Users.Infrastructure.Database;
using Eventify.Modules.Users.Infrastructure.Identity;
using Eventify.Modules.Users.Infrastructure.Users;
using Eventify.Modules.Users.Presentation;
using Eventify.Shared.Application.Authorization;
using Eventify.Shared.Infrastructure.Interceptors;
using Eventify.Shared.Presentation.Endpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Eventify.Modules.Users.Infrastructure;

public static class UsersModule
{
    public static IServiceCollection AddUsersModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddEndpoints(AssemblyReference.Assembly);

        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<UsersDbContext>((sp, options) => options
                .UseNpgsql(
                    configuration.GetConnectionString("Database")!,
                    npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<PublishDomainEventInterceptor>()));

        services.Configure<KeyCloakOptions>(configuration.GetSection("Users:KeyCloak"));
        services.AddHttpClient<KeyCloakClient>((sp, httpClient) =>
                {
                    KeyCloakOptions keyCloakOptions = sp.GetRequiredService<IOptions<KeyCloakOptions>>().Value;
                    httpClient.BaseAddress = new Uri(keyCloakOptions.AdminUrl);
                })
                .AddHttpMessageHandler<KeyCloakAuthDelegatingHandler>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UsersDbContext>());
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPermissionService, PermissionService>();

        services.AddTransient<KeyCloakAuthDelegatingHandler>();
        services.AddTransient<IIdentityProviderService, IdentityProviderService>();
    }
}
