using Eventify.Shared.Presentation.Endpoints;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Modules.Ticketing.Infrastructure;

public static class TicketingModule
{
    public static IServiceCollection AddUsersModule(this IServiceCollection services)
    {
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;
    }
}
