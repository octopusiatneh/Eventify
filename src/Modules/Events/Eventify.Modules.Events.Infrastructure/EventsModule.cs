﻿using System.Reflection;
using Eventify.Modules.Events.Application.Abstractions.Clock;
using Eventify.Modules.Events.Application.Abstractions.Data;
using Eventify.Modules.Events.Application.Abstractions.Database;
using Eventify.Modules.Events.Domain.Categories;
using Eventify.Modules.Events.Domain.Events;
using Eventify.Modules.Events.Domain.TicketTypes;
using Eventify.Modules.Events.Infrastructure.Categories;
using Eventify.Modules.Events.Infrastructure.Clock;
using Eventify.Modules.Events.Infrastructure.Data;
using Eventify.Modules.Events.Infrastructure.Database;
using Eventify.Modules.Events.Infrastructure.Events;
using Eventify.Modules.Events.Infrastructure.TicketTypes;
using Eventify.Modules.Events.Presentation.Events;
using FluentValidation;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;

namespace Eventify.Modules.Events.Infrastructure;

public static class EventsModule
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        EventEndpoints.MapEndpoints(app);
    }

    public static IServiceCollection AddEventsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);
        services.AddInfrastructure(configuration);

        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseConnectionString = configuration.GetConnectionString("Database")!;
        var npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();

        services.TryAddSingleton(npgsqlDataSource);
        
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
        services.AddDbContext<EventsDbContext>(options =>
            options
                .UseNpgsql(
                    databaseConnectionString,
                    npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Events)
                )
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EventsDbContext>());
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
        
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    }
}
