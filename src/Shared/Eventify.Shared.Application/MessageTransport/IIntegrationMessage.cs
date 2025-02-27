namespace Eventify.Shared.Application.MessageTransport;

/// <summary>
/// Represents a message that has integration capabilities between modules.
/// </summary>
public interface IIntegrationMessage
{
    Guid Id { get; }

    DateTime OccurredOnUtc { get; }
}

/// <summary>
/// Represents a message that has integration capabilities between modules.
/// </summary>
public abstract record IntegrationMessage(Guid Id, DateTime OccurredOnUtc) : IIntegrationMessage;
