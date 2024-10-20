using Eventify.Modules.Events.Application.Abstractions.Data;
using Eventify.Modules.Events.Domain.Categories;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Events.Application.Categories.CreateCategory;

public sealed class CreateCategoryHandler : ICommandHandler<CreateCategoryCommand, Guid>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = Category.Create(request.Name);
        await _categoryRepository.InsertAsync(category, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
