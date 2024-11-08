using System.Reflection;
using Eventify.Shared.Application.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Shared.Application;

public static class SharedApplicationConfiguration
{
    public static IServiceCollection AddSharedApplicationConfig(this IServiceCollection services, Assembly[] moduleAssemblies)
    {
        services.AddMediatR(ConfigureMediatr);
        services.AddValidatorsFromAssemblies(moduleAssemblies, includeInternalTypes: true);

        return services;

        void ConfigureMediatr(MediatRServiceConfiguration config)
        {
            config.RegisterServicesFromAssemblies(moduleAssemblies);
            
            config.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(RequestLogPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        }
    }
}
