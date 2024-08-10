namespace InspireMind.Education.Application.Abstractions;

public class Result<TValue>(bool isSuccess, TValue value, Error error)
{
    public bool IsSuccess { get; } = isSuccess;
    public TValue Value { get; } = value;
    public Error Error { get; } = error;
    public bool IsFailure => !IsSuccess;

    public static Result<TValue> Success(TValue value) => new(true, value, Error.None);

    public static Result<TValue> Failure(Error error) => new(false, default!, error);

    public Result<TNew> Bind<TNew>(Func<TValue, Result<TNew>> mappingFn)
         => IsSuccess ? mappingFn(Value) : Result<TNew>.Failure(Error);

}
