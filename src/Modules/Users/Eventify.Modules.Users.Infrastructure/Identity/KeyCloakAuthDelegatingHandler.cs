using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;

namespace Eventify.Modules.Users.Infrastructure.Identity;

public sealed class KeyCloakAuthDelegatingHandler(IOptions<KeyCloakOptions> options) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var authToken = await GetAuthTokenAsync(cancellationToken);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken.AccessToken);

        var responseMessage = await base.SendAsync(request, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();

        return responseMessage;
    }

    private async Task<AuthToken> GetAuthTokenAsync(CancellationToken cancellationToken)
    {
        var keyCloakOptions = options.Value;
        KeyValuePair<string, string>[] authRequestParameters = [
            new("client_id", keyCloakOptions.ConfidentialClientId),
            new("client_secret", keyCloakOptions.ConfidentialClientSecret),
            new("scope", "openid"),
            new("grant_type", "client_credentials"),
        ];

        using FormUrlEncodedContent authRequestContent = new(authRequestParameters);
        using HttpRequestMessage authRequest = new(HttpMethod.Post, keyCloakOptions.TokenUrl)
        {
            Content = authRequestContent
        };
        using var authResponse = await base.SendAsync(authRequest, cancellationToken);
        authResponse.EnsureSuccessStatusCode();

        return await authResponse.Content.ReadFromJsonAsync<AuthToken>(cancellationToken)
            ?? throw new InvalidOperationException("Failed to deserialize auth token");
    }

    internal sealed record AuthToken(
        [property: JsonPropertyName("access_token")] string AccessToken
    );
}
