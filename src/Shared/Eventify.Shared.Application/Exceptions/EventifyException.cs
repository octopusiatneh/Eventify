using Eventify.Shared.Domain;

namespace Eventify.Shared.Application.Exceptions;

public sealed class EventifyException(string requestName, Error? error = default, Exception? innerException = default)
    : Exception("Application exception", innerException)
{
    public string RequestName { get; } = requestName;

    public Error? Error { get; } = error;
}
