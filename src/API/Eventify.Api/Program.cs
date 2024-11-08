using Eventify.Api.Extensions;
using Eventify.Api.Middlewares;
using Eventify.Modules.Events.Infrastructure;
using Eventify.Shared.Application;
using Eventify.Shared.Infrastructure;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, config) => config.ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.CustomSchemaIds(t => t.FullName?.Replace("+", ".")));

// Add cross-cutting concerns
builder.Services.AddSharedApplicationConfig([
    Eventify.Modules.Events.Application.AssemblyReference.Assembly
]);
builder.Services.AddSharedInfrastructureConfig(builder.Configuration);

// Add modules
builder.Services.AddEventsModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

EventsModule.MapEndpoints(app);

app.UseSerilogRequestLogging();
app.UseExceptionHandler();

await app.RunAsync();
