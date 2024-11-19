﻿using Eventify.Modules.Users.Application.Users.Register;
using Eventify.Shared.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Users.Presentation.Users;

internal sealed class RegisterUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users", async (Request request, ISender sender) =>
            {
                var result =
                    await sender.Send(new RegisterUserCommand(request.Email, request.FirstName, request.LastName));

                return result.ToApiResponse(ApiResult.Ok, ApiResult.Problem);
            })
            .WithTags(Tags.Users);
    }

    internal sealed record Request(string Email, string FirstName, string LastName);
}