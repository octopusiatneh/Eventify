using Microsoft.OpenApi.Models;

namespace Eventify.Api.Swagger;

internal static class SwaggerExtensions
{
	internal static IServiceCollection AddEventifySwagger(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen(options =>
		{
			options.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
			options.SwaggerDoc("v1", new OpenApiInfo { Title = "Eventify API", Version = "v1" });

			options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
				Name = "Authorization",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.Http,
				Scheme = "bearer",
				BearerFormat = "JWT"
			});

			var metadataAddress = configuration.GetValue<string>("Authentication:MetadataAddress");
			var authority = metadataAddress?.Replace("/.well-known/openid-configuration", string.Empty);
			var authorizationUrl = authority is null ? null : new Uri($"{authority}/protocol/openid-connect/auth");
			var tokenUrl = configuration.GetValue<string>("Users:KeyCloak:TokenUrl");

			if (authorizationUrl is not null && !string.IsNullOrWhiteSpace(tokenUrl))
			{
				options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
				{
					Type = SecuritySchemeType.OAuth2,
					Flows = new OpenApiOAuthFlows
					{
						AuthorizationCode = new OpenApiOAuthFlow
						{
							AuthorizationUrl = authorizationUrl,
							TokenUrl = new Uri(tokenUrl!),
							Scopes = new Dictionary<string, string>
							{
								["openid"] = "OpenID scope",
								["profile"] = "User profile",
								["email"] = "User email"
							}
						}
					}
				});
			}

			options.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					Array.Empty<string>()
				}
			});

			options.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "OAuth2"
						}
					},
					SwaggerSecurity.OAuthScopes
				}
			});
		});

		return services;
	}
}
