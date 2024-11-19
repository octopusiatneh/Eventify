using Eventify.Shared.Application.Bus;

namespace Eventify.Modules.Users.MessageContracts.IntegrationMessages;

public sealed record UserRegisteredMessage(
    Guid Id,
    DateTime OccurredOnUtc,
    Guid UserId,
    string Email,
    string FirstName,
    string LastName) : IntegrationMessage(Id, OccurredOnUtc);
