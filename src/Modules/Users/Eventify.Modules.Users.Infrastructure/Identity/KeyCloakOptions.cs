namespace Eventify.Modules.Users.Infrastructure.Identity;

public sealed class KeyCloakOptions
{
    public string AdminUrl { get; set; } = string.Empty;

    public string TokenUrl { get; set; } = string.Empty;

    public string ConfidentialClientId { get; set; } = string.Empty;

    public string ConfidentialClientSecret { get; set; } = string.Empty;

    public string PublicClientId { get; set; } = string.Empty;
}
