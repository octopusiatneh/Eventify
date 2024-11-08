using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Users.Presentation.Users;

public static class UserEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        CreateUser.MapEndpoint(app);
    }
}
