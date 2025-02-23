using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Shared.Infrastructure.Authentication;

internal static class AuthenticationExtensions
{
    internal static void AddEventifyAuthentication(this IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
        services.ConfigureOptions<JwtBearerConfigureOptions>();
        services.AddHttpContextAccessor();
    }
}
