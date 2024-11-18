using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Users.Presentation.Users.Create;

public sealed record CreateUserCommand(string Email, string FirstName, string Lastname) : ICommand<Guid>;
