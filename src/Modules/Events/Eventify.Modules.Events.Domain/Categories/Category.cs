using Eventify.Shared.Domain;

namespace Eventify.Modules.Events.Domain.Categories;

public sealed class Category : Entity
{
    private Category()
    {
    }

    public Category(Guid id, string name, bool isArchived)
    {
        Id = id;
        Name = name;
        IsArchived = isArchived;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public bool IsArchived { get; private set; }

    public static Category Create(string name)
        => new(Guid.NewGuid(), name, isArchived: false);
}
