using Eventify.Modules.Events.Application.TicketTypes.CreateTicketType;
using Eventify.Modules.Events.Presentation.WebApi;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Events.Presentation.TicketTypes;

internal static class CreateTicketType
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("ticket-types", async (Request request, ISender sender) =>
        {
            var result = await sender.Send(new CreateTicketTypeCommand(
                request.EventId,
                request.Name,
                request.Price,
                request.Currency,
                request.Quantity));

            return result.ToApiResponse(ApiResults.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.TicketTypes);
    }

    internal sealed record Request(Guid EventId, string Name, decimal Price, string Currency, decimal Quantity);
}
