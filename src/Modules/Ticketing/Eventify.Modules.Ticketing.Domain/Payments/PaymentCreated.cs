using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Domain.Payments;

public sealed class PaymentCreated(Guid paymentId) : DomainEvent
{
    public Guid PaymentId { get; init; } = paymentId;
}
