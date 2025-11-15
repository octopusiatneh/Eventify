using Eventify.Modules.Attendance.Application.Abstraction;
using Eventify.Modules.Attendance.Domain.Attendees;
using Eventify.Modules.Attendance.Domain.Events;
using Eventify.Modules.Attendance.Domain.Tickets;
using Eventify.Modules.Attendance.Infrastructure.Attendees;
using Eventify.Modules.Attendance.Infrastructure.Authentication;
using Eventify.Modules.Attendance.Infrastructure.Database;
using Eventify.Modules.Attendance.Infrastructure.Events;
using Eventify.Modules.Attendance.Infrastructure.Tickets;
using Eventify.Modules.Attendance.Presentation.Attendees;
using Eventify.Modules.Attendance.Presentation.Events;
using Eventify.Modules.Attendance.Presentation.Tickets;
using Eventify.Shared.Infrastructure.Interceptors;
using Eventify.Shared.Presentation.Endpoints;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Modules.Attendance.Infrastructure;

public static class AttendanceModule
{
    public static IServiceCollection AddAttendanceModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;
    }

    public static void ConfigureConsumers(IRegistrationConfigurator configurator)
    {
        configurator.AddConsumer<UserRegisteredIntegrationEventHandler>();
        configurator.AddConsumer<EventPublishedIntegrationEventHandler>();
        configurator.AddConsumer<TicketIssuedIntegrationEventHandler>();
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContextPool<AttendanceDbContext>((sp, options) => options
                .UseNpgsql(
                    configuration.GetConnectionString("Database")!,
                    npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Attendance))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<PublishDomainEventInterceptor>()));

        services.AddScoped<IAttendeeContext, AttendeeContext>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AttendanceDbContext>());
        services.AddScoped<IAttendeeRepository, AttendeeRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
    }
}
