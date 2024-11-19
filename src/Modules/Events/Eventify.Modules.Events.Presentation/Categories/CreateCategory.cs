using Eventify.Modules.Events.Application.Categories.Create;
using Eventify.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Events.Presentation.Categories;

internal sealed class CreateCategory : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("categories", async (Request request, ISender sender) =>
        {
            var result = await sender.Send(new CreateCategoryCommand(request.Name));

            return result.ToApiResponse(ApiResult.Ok, ApiResult.Problem);

        })
        .WithTags(Tags.Categories);
    }

    internal sealed record Request(string Name);
}
