using Eventify.Shared.Domain;
using MediatR;

namespace Eventify.Shared.Application.CQRS;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent> where TDomainEvent : IDomainEvent;
