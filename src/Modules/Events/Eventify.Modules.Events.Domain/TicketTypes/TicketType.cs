using Eventify.Modules.Events.Domain.Events;
using Eventify.Shared.Domain;

namespace Eventify.Modules.Events.Domain.TicketTypes;

public sealed class TicketType : Entity
{
    private TicketType()
    {
    }

    private TicketType(Guid id, Guid eventId, string name, string currency, decimal price, int quantity)
    {
        Id = id;
        EventId = eventId;
        Name = name;
        Currency = currency;
        Price = price;
        Quantity = quantity;
    }

    public Guid Id { get; private set; }

    public Guid EventId { get; private set; }

    public string Name { get; private set; }

    public string Currency { get; private set; }

    public decimal Price { get; private set; }

    public int Quantity { get; private set; }

    public static TicketType Create(Event @event, string name, string currency, decimal price, int quantity)
        => new(Guid.NewGuid(), @event.Id, name, currency, price, quantity);

    public void UpdatePrice(decimal price)
    {
        if (Price == price)
        {
            return;
        }

        Price = price;
    }
}
