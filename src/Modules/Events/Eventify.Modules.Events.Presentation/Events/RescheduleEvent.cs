using Eventify.Modules.Events.Application.Events.Reschedule;
using Eventify.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Events.Presentation.Events;

internal sealed class RescheduleEvent : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("events/{id}/reschedule", async (Guid id, Request request, ISender sender) =>
        {
            var command = new RescheduleEventCommand(id, request.StartsAtUtc, request.EndsAtUtec);
            var result = await sender.Send(command);

            return result.ToApiResponse(ApiResult.NoContent, ApiResult.Problem);
        })
        .WithTags(Tags.Events);
    }

    internal sealed record Request(DateTime? StartsAtUtc, DateTime? EndsAtUtec);
}
