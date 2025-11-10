namespace WebArchitecturesExamples.Clean.Utils;

public struct OkOrError<TError>
{
    private readonly TError error;
    private readonly bool isError;

    public bool IsOk => !isError;
    public bool IsError => isError;

    public TError GetError() => isError ? error : throw new InvalidOperationException("No error available");

    // Private constructor
    private OkOrError(TError error, bool isError)
    {
        this.error = error;
        this.isError = isError;
    }

    public static OkOrError<TError> Ok() => new OkOrError<TError>(default!, false);

    // Static factory method for error
    public static OkOrError<TError> Error(TError error) => new OkOrError<TError>(error, true);

    public override string ToString()
    {
        return IsOk ? "IsOk" : error.ToString();
    }
}