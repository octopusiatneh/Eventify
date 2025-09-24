using Eventify.Modules.Ticketing.Application.Abstractions;
using Eventify.Modules.Ticketing.Domain.Customers;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Application.Customers.Create;

internal sealed class CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCustomerCommand>
{
    public async Task<Result> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var (customerId, email, firstName, lastName) = request;
        var customer = Customer.Create(customerId, email, firstName, lastName);

        await customerRepository.InsertAsync(customer, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
