using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Shared.Infrastructure.Authorization;

internal static class AuthorizationExtensions
{
    internal static IServiceCollection AddEventifyAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization();

        services.AddTransient<IClaimsTransformation, EventifyClaimsTransformation>();
        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        return services;
    }
}
