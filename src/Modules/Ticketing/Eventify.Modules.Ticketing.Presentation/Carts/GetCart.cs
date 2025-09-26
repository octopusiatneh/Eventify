using Eventify.Modules.Ticketing.Application.Abstractions.Authentication;
using Eventify.Modules.Ticketing.Application.Carts.Get;
using Eventify.Shared.Presentation.ApiResult;
using Eventify.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Ticketing.Presentation.Carts;

internal sealed class GetCart : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("carts", async (ICustomerContext customerContext, ISender sender) =>
        {
            var result = await sender.Send(new GetCartQuery(customerContext.CustomerId));

            return result.ToApiResponse(ApiResult.Ok, ApiResult.Problem);
        })
        .WithTags(Tags.Cart);
    }

}
