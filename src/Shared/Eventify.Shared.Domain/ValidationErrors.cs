namespace Eventify.Shared.Domain;

public sealed record ValidationErrors(Error[] Errors) : Error("General.Validation",
    "One or more validation errors occurred",
    ErrorType.Validation)
{
    public static ValidationErrors FromResults(IEnumerable<Result> results) =>
        new(results.Where(r => r.IsFailure).Select(r => r.Error).ToArray());
}
