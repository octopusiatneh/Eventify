using Eventify.Modules.Events.Application.Events.Create;
using Eventify.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Events.Presentation.Events;

internal sealed class CreateEvent : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("events", async (Request request, ISender sender) =>
        {
            var (categoryId, title, description, location, startsAtUtc, endsAtUtc) = request;
            var command = new CreateEventCommand(categoryId, title, description, location, startsAtUtc, endsAtUtc);
            var result = await sender.Send(command);

            return result.ToApiResponse(ApiResult.Ok, ApiResult.Problem);
        })
        .WithTags(Tags.Events);
    }

    internal sealed record Request(
        Guid CategoryId,
        string Title,
        string Description,
        string Location,
        DateTime StartsAtUtc,
        DateTime? EndsAtUtc
    );
}
