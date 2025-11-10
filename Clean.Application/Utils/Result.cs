namespace WebArchitecturesExamples.Clean.Utils;

public readonly struct Result<TValue, TError>
{
    private readonly TValue? value;
    private readonly TError? error;
    private readonly bool isError;

    public bool IsOk => !isError;
    public bool IsError => isError;

    public TError GetError() => isError ? error! : throw new InvalidOperationException("No error available");

    public TValue? GetValue() => isError ? throw new InvalidOperationException("No value available") : value;

    private Result(TValue? value, TError? error, bool isError)
    {
        this.value = value;
        this.error = error;
        this.isError = isError;
    }

    public Result<TNewValue, TError> MapValue<TNewValue>(Func<TValue, TNewValue> func) => isError
        ? new(default, error, isError)
        : new(func(value!), default, isError);

    public Result<TValue, TNewError> MapError<TNewError>(Func<TError, TNewError> func) => isError
        ? new(default, func(error!), isError)
        : new(value, default, isError);

    public static Result<TValue, TError> Ok(TValue value) => new(value, default, false);

    public static Result<TValue, TError> Error(TError error) => new(default, error, true);

    public override string ToString()
    {
        return IsOk ? "IsOk" : error.ToString();
    }
}