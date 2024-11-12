using Eventify.Api.Extensions;
using Eventify.Api.Middlewares;
using Eventify.Modules.Events.Infrastructure;
using Eventify.Modules.Users.Infrastructure;
using Eventify.Shared.Application;
using Eventify.Shared.Infrastructure;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, config) => config.ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.CustomSchemaIds(t => t.FullName?.Replace("+", ".")));

// Add Cross-Cutting concerns
builder.Services.AddSharedApplicationConfig([
    Eventify.Modules.Events.Application.AssemblyReference.Assembly,
    Eventify.Modules.Users.Application.AssemblyReference.Assembly,
]);
builder.Services.AddSharedInfrastructureConfig(builder.Configuration);

// Add Modules
builder.Services.AddEventsModule(builder.Configuration);
builder.Services.AddUsersModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

EventsModule.MapEndpoints(app);
UsersModule.MapEndpoints(app);

app.UseSerilogRequestLogging();
app.UseExceptionHandler();

await app.RunAsync();
