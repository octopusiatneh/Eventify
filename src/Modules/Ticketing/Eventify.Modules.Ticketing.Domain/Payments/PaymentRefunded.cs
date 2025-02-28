﻿using Eventify.Shared.Domain;

namespace Eventify.Modules.Ticketing.Domain.Payments;

public sealed class PaymentRefunded(Guid paymentId, Guid transactionId, decimal refundAmount)
    : DomainEvent
{
    public Guid PaymentId { get; init; } = paymentId;

    public Guid TransactionId { get; init; } = transactionId;

    public decimal RefundAmount { get; init; } = refundAmount;
}
