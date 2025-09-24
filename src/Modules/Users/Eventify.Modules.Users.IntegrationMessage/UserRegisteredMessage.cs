using Eventify.Shared.Application.MessageTransport;

namespace Eventify.Modules.Users.IntegrationMessages;

public sealed record UserRegisteredMessage(
    Guid Id,
    DateTime OccurredOnUtc,
    Guid UserId,
    string Email,
    string FirstName,
    string LastName) : IntegrationMessage(Id, OccurredOnUtc);
