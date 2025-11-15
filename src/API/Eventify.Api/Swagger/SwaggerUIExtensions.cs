namespace Eventify.Api.Swagger;

internal static class SwaggerUIExtensions
{
    internal static void UseEventifySwaggerUI(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Eventify API v1");
            c.OAuthClientId(configuration.GetValue<string>("Users:KeyCloak:PublicClientId"));
            c.OAuthUsePkce();
            c.OAuthScopes(SwaggerSecurity.OAuthScopes);
            c.OAuthAppName("Eventify Swagger");
        });
    }
}
