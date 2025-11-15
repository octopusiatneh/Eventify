using Eventify.Modules.Events.Application.Events.PublishEvent;
using Eventify.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Events.Presentation.Events;

internal sealed class PublishEvent : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("events/{id}/publish", async (Guid id, ISender sender) =>
        {
            var command = new PublishEventCommand(id);
            var result = await sender.Send(command);

            return result.ToApiResponse(ApiResult.NoContent, ApiResult.Problem);
        })
        .WithTags(Tags.Events);
    }
}
