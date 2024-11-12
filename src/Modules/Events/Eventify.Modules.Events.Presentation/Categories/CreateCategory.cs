using Eventify.Modules.Events.Application.Categories.CreateCategory;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Events.Presentation.Categories;

internal static class CreateCategory
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("categories", async (Request request, ISender sender) =>
        {
            var result = await sender.Send(new CreateCategoryCommand(request.Name));

            return result.ToApiResponse(ApiResults.Ok, ApiResults.Problem);

        })
        .WithTags(Tags.Categories);
    }

    internal sealed record Request(string Name);
}
