using Eventify.Modules.Users.Application.Users.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Users.Presentation.Users;

internal static class CreateUser
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users", async (Request request, ISender sender) =>
        {
            var result = await sender.Send(new CreateUserCommand(request.Email, request.FirstName, request.LastName));

            return result.ToApiResponse(ApiResults.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Users);
    }

    internal sealed record Request(string Email, string FirstName, string LastName);
}
