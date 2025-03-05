using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Eventify.Shared.Infrastructure.Authentication;

internal sealed class JwtCreationOptions(IConfiguration configuration) : IConfigureNamedOptions<JwtBearerOptions>
{
    private const string ConfigurationSectionName = "Authentication";

    public void Configure(string? name, JwtBearerOptions options)
        => configuration.GetSection(ConfigurationSectionName).Bind(options);

    // This method is required by the interface but not used in this scenario.
    public void Configure(JwtBearerOptions options)
        => Configure(Options.DefaultName, options);
}
