using Eventify.Shared.Domain;

namespace Eventify.Shared.Presentation.ApiResult;

public static class ResultExtensions
{
    public static TOut ToApiResponse<TOut>(this Result result, Func<TOut> onSuccess, Func<Result, TOut> onFailure)
        => result.IsSuccess ? onSuccess() : onFailure(result);

    public static TOut ToApiResponse<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> onSuccess, Func<Result<TIn>, TOut> onFailure)
        => result.IsSuccess ? onSuccess(result.Value) : onFailure(result);
}
