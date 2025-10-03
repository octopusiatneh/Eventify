using Eventify.Modules.Ticketing.Application.Customers.GetById;
using Eventify.Shared.Presentation.ApiResult;
using Eventify.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Ticketing.Presentation.Customers;

internal sealed class GetCustomerById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("customers/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetCustomerByIdQuery(id);
            var result = await sender.Send(query);

            return result.ToApiResponse(ApiResult.Ok, ApiResult.Problem);
        })
        .WithTags(Tags.Customers);
    }
}
