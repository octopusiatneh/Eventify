using Eventify.Modules.Events.Domain.Categories;
using Eventify.Modules.Events.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventify.Modules.Events.Infrastructure.Categories;

internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasMany<Event>().WithOne();
    }
}
