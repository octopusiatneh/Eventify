using Eventify.Modules.Events.Application.Abstractions.CQRS;

namespace Eventify.Modules.Events.Application.Categories.CreateCategory;

public sealed record CreateTicketTypeCommand(string Name) : ICommand<Guid>;
