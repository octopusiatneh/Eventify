using Eventify.Api.Extensions;
using Eventify.Api.Middlewares;
using Eventify.Api.Swagger;
using Eventify.Modules.Events.Infrastructure;
using Eventify.Modules.Ticketing.Infrastructure;
using Eventify.Modules.Users.Infrastructure;
using Eventify.Shared.Application;
using Eventify.Shared.Infrastructure;
using Eventify.Shared.Presentation.Endpoints;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, config) => config.ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEventifySwagger(builder.Configuration);

// Add Cross-Cutting concerns
builder.Services.AddSharedApplicationConfig([
    Eventify.Modules.Events.Application.AssemblyReference.Assembly,
    Eventify.Modules.Users.Application.AssemblyReference.Assembly,
    Eventify.Modules.Ticketing.Application.AssemblyReference.Assembly,
]);
builder.Services.AddSharedInfrastructureConfig("Eventify.Api", builder.Configuration, [TicketingModule.ConfigureConsumers]);

// Add Health Check
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!)
    .AddUrlGroup(new Uri(builder.Configuration.GetValue<string>("KeyCloak:HealthUrl")!), httpMethod: HttpMethod.Get, "keycloark");

// Add Modules
builder.Services.AddEventsModule(builder.Configuration);
builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddTicketingModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseEventifySwaggerUI(builder.Configuration);
    app.ApplyMigrations();
}

app.MapEndpoints();
app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseMiddleware<LogContextTraceLoggingMiddleware>();

app.UseSerilogRequestLogging();
app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
