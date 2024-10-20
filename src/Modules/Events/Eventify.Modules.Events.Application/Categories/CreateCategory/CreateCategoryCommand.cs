using Eventify.Shared.Application.CQRS;

namespace Eventify.Modules.Events.Application.Categories.CreateCategory;

public sealed record CreateCategoryCommand(string Name) : ICommand<Guid>;
