using Eventify.Modules.Ticketing.Application.Abstractions.Authentication;
using Eventify.Modules.Ticketing.Application.Carts.RemoveItem;
using Eventify.Shared.Presentation.ApiResult;
using Eventify.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Ticketing.Presentation.Carts;

internal sealed class RemoveItemFromCart : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("carts/remove", async (Request request, ICustomerContext customerContext, ISender sender) =>
        {
            var result = await sender.Send(
                new RemoveItemFromCartCommand(customerContext.CustomerId, request.TicketTypeId)
            );

            return result.ToApiResponse(Results.NoContent, ApiResult.Problem);
        })
        .WithTags(Tags.Carts);
    }

    internal sealed record Request(Guid TicketTypeId);
}
