namespace Eventify.Modules.Events.Domain.Abstractions;

public record Error(string Code, string Message, ErrorType Type)
{
    public static Error None => new(string.Empty, string.Empty, ErrorType.None);

    public static Error NullValue => new("General.Null", "Null value provided", ErrorType.Problem);

    public static Error NotFound(string code, string message) => new(code, message, ErrorType.NotFound);

    public static Error Failure(string code, string message) => new(code, message, ErrorType.Problem);

    public static Error Problem(string code, string message) => new(code, message, ErrorType.Problem);

    public static Error Conflict(string code, string message) => new(code, message, ErrorType.Conflict);
}
