using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Eventify.Shared.Infrastructure.OpenTelemetry;

public static class OpenTelemetryExtensions
{
    public static void AddDistributedTracing(this IServiceCollection services, string serviceName)
    {
        services
            .AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(serviceName))
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation()
                    .AddRedisInstrumentation()
                    .AddNpgsql()
                    .AddSource(MassTransit.Logging.DiagnosticHeaders.DefaultListenerName);

                tracing.AddOtlpExporter();
            });

    }
}
