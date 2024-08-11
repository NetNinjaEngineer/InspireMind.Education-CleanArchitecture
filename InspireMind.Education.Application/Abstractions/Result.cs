namespace InspireMind.Education.Application.Abstractions;

public class Result<TValue>(bool isSuccess, TValue value, IEnumerable<string>? errors = null)
{
    public bool IsSuccess { get; } = isSuccess;
    public TValue Value { get; } = value;
    public IEnumerable<string>? Errors { get; } = errors;
    public bool IsFailure => !IsSuccess;

    public static Result<TValue> Success(TValue value) => new(true, value);

    public static Result<TValue> Failure(IEnumerable<string> errors) => new(false, default!, errors);

    public Result<TNew> Bind<TNew>(Func<TValue, Result<TNew>> mappingFn)
         => IsSuccess ? mappingFn(Value) : Result<TNew>.Failure(Errors);

}
