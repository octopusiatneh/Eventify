using Eventify.Modules.Events.Application.TicketTypes.Create;
using Eventify.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Events.Presentation.TicketTypes;

internal sealed class CreateTicketType : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("ticket-types", async (Request request, ISender sender) =>
        {
            var result = await sender.Send(new CreateTicketTypeCommand(
                request.EventId,
                request.Name,
                request.Price,
                request.Currency,
                request.Quantity));

            return result.ToApiResponse(ApiResult.Ok, ApiResult.Problem);
        })
        .WithTags(Tags.TicketTypes);
    }

    internal sealed record Request(Guid EventId, string Name, decimal Price, string Currency, int Quantity);
}
