﻿using Microsoft.AspNetCore.Routing;

namespace Eventify.Modules.Events.Presentation.Categories;

public static class CategoryEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        CreateCategory.MapEndpoint(app);
    }
}
