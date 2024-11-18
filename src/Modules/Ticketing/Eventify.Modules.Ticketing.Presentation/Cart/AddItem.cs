using Eventify.Modules.Ticketing.Application.Cart.AddItem;
using Eventify.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Ticketing.Presentation.Cart;

internal sealed class AddItem : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("cart", async (Request request, ISender sender) =>
        {
            var (eventId, ticketTypeId, quantity) = request;
            var command = new AddItemCommand(eventId, ticketTypeId, quantity);
            await sender.Send(command);
        }).WithTags(Tags.Cart);
    }

    internal sealed record Request(
        Guid EventId,
        Guid TicketTypeId,
        int Quantity
    );
}
