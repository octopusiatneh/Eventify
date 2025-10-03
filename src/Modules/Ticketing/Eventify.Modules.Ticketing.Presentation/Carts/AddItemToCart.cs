using Eventify.Modules.Ticketing.Application.Abstractions.Authentication;
using Eventify.Modules.Ticketing.Application.Carts.AddItem;
using Eventify.Shared.Presentation.ApiResult;
using Eventify.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Ticketing.Presentation.Carts;

internal sealed class AddItem : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("carts/add", async (Request request, ICustomerContext customerContext, ISender sender) =>
        {
            var customerId = customerContext.CustomerId;
            var (ticketTypeId, quantity) = request;
            var command = new AddItemToCartCommand(customerId, ticketTypeId, quantity);

            var result = await sender.Send(command);

            return result.ToApiResponse(ApiResult.NoContent, ApiResult.Problem);
        }).WithTags(Tags.Carts);
    }

    internal sealed record Request(
        Guid TicketTypeId,
        int Quantity
    );
}
