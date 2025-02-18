using System.Net.Http.Json;

namespace Eventify.Modules.Users.Infrastructure.Identity;

internal sealed class KeyCloakClient(HttpClient httpClient)
{
    internal async Task<string> RegisterUserAsync(UserRepresentation userRepresentation, CancellationToken cancellationToken = default)
    {
        var httpResponseMessage = await httpClient.PostAsJsonAsync("users", userRepresentation, cancellationToken);
        httpResponseMessage.EnsureSuccessStatusCode();

        return ExtractIdentityIdFromLocationHeader(httpResponseMessage);
    }

    private static string ExtractIdentityIdFromLocationHeader(HttpResponseMessage httpResponseMessage)
    {
        const string userSegmentName = "users/";
        string? locationHeader = httpResponseMessage.Headers.Location?.PathAndQuery;
        if (locationHeader is null)
        {
            throw new InvalidOperationException("Location header is missing.");
        }

        int userSegmentValueIndex = locationHeader.IndexOf(userSegmentName, StringComparison.InvariantCultureIgnoreCase);
        string identityId = locationHeader[(userSegmentValueIndex + userSegmentName.Length)..];

        return identityId;
    }
}
