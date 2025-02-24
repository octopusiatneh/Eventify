using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Shared.Infrastructure.Authentication;

internal static class AuthenticationExtensions
{
    internal static void AddEventifyAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication().AddJwtBearer();
        services.AddHttpContextAccessor();
        services.ConfigureOptions<JwtBearerConfigureOptions>();
    }
}
