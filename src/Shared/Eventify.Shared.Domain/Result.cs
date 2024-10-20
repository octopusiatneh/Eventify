using System.Diagnostics.CodeAnalysis;

namespace Eventify.Shared.Domain;

public class Result
{
    public Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; private set; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; private set; }

    public static Result Success() => new(true, Error.None);

    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

    public static Result Failure(Error error) => new(false, error);

    public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
}

public class Result<TValue> : Result
{
    public Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }

    private readonly TValue? _value;

    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can't be accessed");

    public static implicit operator Result<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);

    public static Result<TValue> ValidationFailure(Error error) =>
        new(default, false, error);
}
