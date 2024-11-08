using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Users.Application.Users.CreateUser;

public sealed record CreateUserCommand(string Email, string FirstName, string Lastname) : ICommand<Guid>;
