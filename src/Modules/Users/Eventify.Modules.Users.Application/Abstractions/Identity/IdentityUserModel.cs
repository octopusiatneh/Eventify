namespace Eventify.Modules.Users.Application.Abstractions.Identity;

public sealed record IdentityUserModel(string Email, string Password, string FirstName, string LastName);
