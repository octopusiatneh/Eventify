using Eventify.Modules.Ticketing.Application.Abstractions.Authentication;
using Eventify.Modules.Ticketing.Application.Carts.Clear;
using Eventify.Shared.Presentation.ApiResult;
using Eventify.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Ticketing.Presentation.Carts;

internal sealed class ClearCart : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("carts", async (ICustomerContext customerContext, ISender sender) =>
        {
            var result = await sender.Send(new ClearCartCommand(customerContext.CustomerId));

            return result.ToApiResponse(ApiResult.NoContent, ApiResult.Problem);
        })
        .WithTags(Tags.Cart);
    }

    internal sealed record Request(
        Guid CustomerId
    );
}
