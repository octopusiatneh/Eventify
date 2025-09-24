using System.Data.Common;
using Eventify.Modules.Ticketing.Application.Abstractions;
using Eventify.Modules.Ticketing.Application.Abstractions.Carts;
using Eventify.Modules.Ticketing.Application.Abstractions.Payments;
using Eventify.Modules.Ticketing.Domain.Customers;
using Eventify.Modules.Ticketing.Domain.Orders;
using Eventify.Modules.Ticketing.Domain.Payments;
using Eventify.Modules.Ticketing.Domain.TicketTypes;
using Eventify.Shared.Application.CQRS;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Application.Orders.Create;

internal sealed class CreateOrderCommandHandler(
    ICustomerRepository customerRepository,
    ITicketTypeRepository ticketTypeRepository,
    IOrderRepository orderRepository,
    IPaymentRepository paymentRepository,
    ICartService cartService,
    IPaymentService paymentService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateOrderCommand>
{
    public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        await using DbTransaction transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

        var customerId = request.CustomerId;
        var customer = await customerRepository.GetAsync(customerId, cancellationToken);
        if (customer is null)
        {
            return Result.Failure(CustomerErrors.NotFound(customerId));
        }

        var order = Order.Create(customer!);
        var cart = await cartService.GetAsync(customerId, cancellationToken);
        if (cart.Items.Count == 0)
        {
            return Result.Failure(OrderErrors.EmptyCart);
        }

        foreach (var item in cart.Items)
        {
            var ticketType = await ticketTypeRepository.GetWithLockAsync(item.TicketTypeId, cancellationToken);
            if (ticketType is null)
            {
                return Result.Failure(TicketTypeErrors.NotFound(item.TicketTypeId));
            }

            var updateTicketTypeQuantityResult = ticketType.UpdateQuantity(item.Quantity);
            if (updateTicketTypeQuantityResult.IsFailure)
            {
                return Result.Failure(updateTicketTypeQuantityResult.Error);
            }

            order.AddItem(ticketType, item.Quantity, ticketType.Price, ticketType.Currency);
        }
        orderRepository.Insert(order);

        // Faking a payment gateway request here...
        var paymentResponse = await paymentService.ChargeAsync(order.TotalPrice, order.Currency);
        var payment = Payment.Create(
            order,
            paymentResponse.TransactionId,
            paymentResponse.Amount,
            paymentResponse.Currency);
        paymentRepository.Insert(payment);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        await cartService.ClearAsync(customerId, cancellationToken);

        return Result.Success();
    }
}
