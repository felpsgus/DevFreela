namespace DevFreela.Application.Abstractions;

public class Result
{
    protected Result(bool isSuccess = true, IEnumerable<Error>? errors = null)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; private set; }

    public IEnumerable<Error>? Errors { get; private set; }

    public static Result Success() => new();

    public static Result Error(Error error) => new(false, [error]);

    public static Result Error(IEnumerable<Error> errors) => new(false, errors);

    public static implicit operator Result(Error error) => Error(error);
}

public class Result<T> : Result
{
    protected Result(T? data, bool isSuccess = true, IEnumerable<Error>? errors = null) : base(isSuccess, errors)
    {
        Data = data;
    }

    public T? Data { get; private set; }

    public static Result<T> Success(T data) => new(data);

    public static Result<T> Error(Error error) => new(default, false, [error]);

    public static Result<T> Error(IEnumerable<Error> errors) => new(default, false, errors);

    public static implicit operator Result<T>(T data) => Success(data);

    public static implicit operator Result<T>(Error error) => Error(error);
}

public class ValidationResult : Result
{
    private ValidationResult(bool isSuccess = true, IEnumerable<Error>? errors = null) : base(isSuccess, errors)
    {
    }

    public static ValidationResult WithErrors(IEnumerable<Error> errors) => new(false, errors);
}

public class ValidationResult<T> : Result<T>
{
    private ValidationResult(T? data, bool isSuccess = true, IEnumerable<Error>? errors = null) : base(data, isSuccess,
        errors)
    {
    }

    public static ValidationResult<T> WithErrors(IEnumerable<Error> errors) => new(default, false, errors);
}

public sealed record Error(string Code, string Message);