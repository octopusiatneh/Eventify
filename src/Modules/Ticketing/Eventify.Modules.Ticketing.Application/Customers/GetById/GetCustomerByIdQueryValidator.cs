using FluentValidation;

namespace Eventify.Modules.Ticketing.Application.Customers.GetById;

internal sealed class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
{
    public GetCustomerByIdQueryValidator()
    {
        RuleFor(c => c.CustomerId).NotEmpty();
    }
}
