using System.Security.Claims;
using Eventify.Modules.Users.Application.Users.Get;
using Eventify.Shared.Application.Authentication;
using Eventify.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Users.Presentation.Users;

internal sealed class GetUserProfile : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/profile", async (ClaimsPrincipal claims, ISender sender) =>
        {
            var getUserResult = await sender.Send(new GetUserByIdQuery(claims.GetUserId()));

            return getUserResult.ToApiResponse(ApiResult.Ok, ApiResult.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.Users);
    }
}
