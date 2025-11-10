namespace WebArchitecturesExamples.Clean.Utils;

public abstract class StatusResponse<TError, TSelf> where TSelf : StatusResponse<TError, TSelf>, new()
{
    public OkOrError<TError> Status { get; init; }

    public static TSelf Error(TError status)
    {
        return new TSelf()
        {
            Status = OkOrError<TError>.Error(status)
        };
    }

    public static TSelf Ok() => new()
    {
        Status = OkOrError<TError>.Ok()
    };
}

public abstract class ResultResponse<TValue, TError, TSelf> where TSelf : ResultResponse<TValue, TError, TSelf>, new()
{
    public Result<TValue, TError> Result { get; init; }

    public static TSelf Error(TError error)
    {
        return new TSelf()
        {
            Result = Result<TValue, TError>.Error(error)
        };
    }

    public static TSelf Ok(TValue value)
    {
        return new TSelf()
        {
            Result = Result<TValue, TError>.Ok(value)
        };
    }
}