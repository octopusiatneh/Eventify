using Eventify.Modules.Events.Application.Categories.CreateCategory;
using Eventify.Modules.Events.Presentation.WebApi;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Events.Presentation.Categories;

internal static class CreateCategory
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("categories", async (Request request, ISender sender) =>
        {
            var result = await sender.Send(new CreateTicketTypeCommand(request.Name));

            return result.ToApiResponse(ApiResults.Ok, ApiResults.Problem);

        });
    }

    internal sealed record Request(string Name);
}
