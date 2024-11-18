using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Application.Cart.AddItem;

internal sealed class AddItemHandler : ICommandHandler<AddItemCommand>
{
    public Task<Result> Handle(AddItemCommand request, CancellationToken cancellationToken)
        => throw new NotImplementedException();
}
