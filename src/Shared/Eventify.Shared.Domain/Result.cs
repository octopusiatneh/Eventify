using System.Diagnostics.CodeAnalysis;

namespace Eventify.Shared.Domain;

public class Result(bool isSuccess, Error error)
{
    public bool IsSuccess { get; private set; } = isSuccess;

    public bool IsFailure => !IsSuccess;

    public Error Error { get; private set; } = error;

    public static Result Success() => new(true, Error.None);

    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

    public static Result Failure(Error error) => new(false, error);

    public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
}

public class Result<TValue>(TValue? value, bool isSuccess, Error error)
    : Result(isSuccess, error)
{
    [NotNull]
    public TValue Value => IsSuccess
        ? value!
        : throw new InvalidOperationException("The value of a failure result can't be accessed");

    public TResponse Match<TResponse>(Func<TValue, TResponse> onSuccess, Func<Error, TResponse> onFailure)
        => IsSuccess ? onSuccess(Value) : onFailure(Error);

    public async Task<TResponse> Match<TResponse>(Func<TValue, Task<TResponse>> onSuccess, Func<Error, Task<TResponse>> onFailure)
        => IsSuccess ? await onSuccess(Value) : await onFailure(Error);

    public static Result<TValue> ValidationFailure(Error error)
        => new(default, false, error);

    public static implicit operator Result<TValue>(TValue? value)
        => value is not null ? Success(value) : Failure<TValue>(Error.NullValue);

    public static implicit operator Result<TValue>(Error error)
        => Failure<TValue>(error);
}
