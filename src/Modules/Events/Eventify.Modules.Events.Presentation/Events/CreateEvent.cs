using Eventify.Modules.Events.Application.Events.CreateEvent;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Events.Presentation.Events;

internal static class CreateEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("events", async (Request request, ISender sender) =>
        {
            var (title, description, location, startsAtUtc, endsAtUtc) = request;
            var command = new CreateEventCommand(title, description, location, startsAtUtc, endsAtUtc);
            var eventId = await sender.Send(command);

            return Results.Ok(eventId);
        })
        .WithTags(Tags.Events);
    }

    internal sealed record Request(
        string Title,
        string Description,
        string Location,
        DateTime StartsAtUtc,
        DateTime? EndsAtUtc
    );
}
