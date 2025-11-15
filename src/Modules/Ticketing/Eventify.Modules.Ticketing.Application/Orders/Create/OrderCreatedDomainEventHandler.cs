using Eventify.Modules.Ticketing.Application.Orders.Get;
using Eventify.Modules.Ticketing.Domain.Orders;
using Eventify.Modules.Ticketing.IntegrationEvents;
using Eventify.Shared.Application.EventBus;
using Eventify.Shared.Application.Exceptions;
using MediatR;

namespace Eventify.Modules.Ticketing.Application.Orders.Create;

public class OrderCreatedDomainEventHandler(ISender sender, IEventBus eventBus) : INotificationHandler<OrderCreatedDomainEvent>
{
    public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetOrderQuery(notification.OrderId), cancellationToken);

        if (result.IsFailure)
        {
            throw new EventifyException(nameof(GetOrderQuery), result.Error);
        }

        await eventBus.PublishAsync(
            new OrderCreatedIntegrationEvent(
                notification.Id,
                notification.OccurredOnUtc,
                result.Value.Id,
                result.Value.CustomerId,
                result.Value.TotalPrice,
                result.Value.CreatedAtUtc,
                result.Value.OrderItems.Select(oi => new OrderItemModel(
                    oi.OrderItemId,
                    result.Value.Id,
                    oi.TicketTypeId,
                    oi.Quantity,
                    oi.UnitPrice,
                    oi.Price,
                    oi.Currency
                )).ToList()),
            cancellationToken);
    }
}
