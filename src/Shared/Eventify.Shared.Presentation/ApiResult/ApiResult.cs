using Eventify.Shared.Domain;
using Microsoft.AspNetCore.Http;

namespace Eventify.Shared.Presentation.ApiResult;

public sealed class ApiResult
{
    public static IResult Ok<TValue>(TValue? value = default)
        => value is null ? Results.Ok() : Results.Ok(value);

    public static IResult NoContent() => Results.NoContent();

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
            extensions: GetErrors(result.Error)
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

        static Dictionary<string, object?>? GetErrors(Error error)
        {
            if (error is not ValidationError validationErrors)
            {
                return null;
            }

            var dictionary = new Dictionary<string, object?>
            {
                {
                    "extensions",
                    validationErrors.Errors
                        .GroupBy(e => e.Code, e => e.Message)
                        .ToDictionary(e => e.Key, e => e.ToArray())
                }
            };

            return dictionary;
        }
    }
}
