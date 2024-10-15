﻿using Eventify.Modules.Events.Domain.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Eventify.Modules.Events.Presentation.WebApi;

public sealed class ApiResults
{
    public static IResult Ok() => Results.Ok();

    public static IResult Ok<TValue>(Result<TValue> result) => Results.Ok(result.Value);

    public static IResult Problem(Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        return Results.Problem(
            title: GetTitle(result.Error),
            detail: GetDetail(result.Error),
            type: GetType(result.Error.Type),
            statusCode: GetStatusCode(result.Error.Type),
            extensions: GetErrors()
        );

        static string GetTitle(Error error) =>
            error.Type switch
            {
                ErrorType.None => error.Code,
                ErrorType.Validation => error.Code,
                ErrorType.Problem => error.Code,
                ErrorType.NotFound => error.Code,
                ErrorType.Conflict => error.Code,
                _ => "Server failure"
            };

        static string GetDetail(Error error) =>
            error.Type switch
            {
                ErrorType.None => error.Message,
                ErrorType.Validation => error.Message,
                ErrorType.Problem => error.Message,
                ErrorType.NotFound => error.Message,
                ErrorType.Conflict => error.Message,
                _ => "An unexpected error occurred"
            };

        static string GetType(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.None => "",
                ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ErrorType.Problem => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

        static int GetStatusCode(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.None => StatusCodes.Status204NoContent,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Problem => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

        static Dictionary<string, object?>? GetErrors() => null;
    }
}
