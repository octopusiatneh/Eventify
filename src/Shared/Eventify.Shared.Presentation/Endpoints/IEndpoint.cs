using Microsoft.AspNetCore.Routing;

namespace Eventify.Shared.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
