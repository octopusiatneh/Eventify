using Eventify.Modules.Ticketing.Application.Abstractions;
using Eventify.Modules.Ticketing.Application.Abstractions.Carts;
using Eventify.Modules.Ticketing.Domain.Customers;
using Eventify.Modules.Ticketing.Domain.Events;
using Eventify.Modules.Ticketing.Domain.Orders;
using Eventify.Modules.Ticketing.Domain.Payments;
using Eventify.Modules.Ticketing.Domain.Tickets;
using Eventify.Modules.Ticketing.Domain.TicketTypes;
using Eventify.Modules.Ticketing.Infrastructure.Carts;
using Eventify.Modules.Ticketing.Infrastructure.Customers;
using Eventify.Modules.Ticketing.Infrastructure.Database;
using Eventify.Modules.Ticketing.Infrastructure.Events;
using Eventify.Modules.Ticketing.Infrastructure.Orders;
using Eventify.Modules.Ticketing.Infrastructure.Payments;
using Eventify.Modules.Ticketing.Infrastructure.Tickets;
using Eventify.Modules.Ticketing.Infrastructure.TicketTypes;
using Eventify.Modules.Ticketing.Presentation;
using Eventify.Modules.Ticketing.Presentation.Customers;
using Eventify.Shared.Infrastructure.Interceptors;
using Eventify.Shared.Presentation.Endpoints;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Modules.Ticketing.Infrastructure;

public static class TicketingModule
{
    public static IServiceCollection AddTicketingModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddEndpoints(AssemblyReference.Assembly);

        return services;
    }

    public static void ConfigureConsumers(IRegistrationConfigurator configurator)
    {
        configurator.AddConsumer<UserRegisteredConsumer>();
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContextPool<TicketingDbContext>((sp, options) => options
                .UseNpgsql(
                    configuration.GetConnectionString("Database"),
                    npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Ticketing))
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<PublishDomainEventInterceptor>()));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<TicketingDbContext>());
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
        services.AddScoped<ICartService, CartService>();
    }
}
