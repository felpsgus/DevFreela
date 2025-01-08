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

    public static Result Success() => new Result();

    public static Result Error(Error error) => new Result(false, [error]);

    public static Result Error(IEnumerable<Error> errors) => new Result(false, errors);

    public static implicit operator Result(Error error) => Error(error);
}

public class Result<T> : Result
{
    private Result(T? data, bool isSuccess = true, IEnumerable<Error>? errors = null) : base(isSuccess, errors)
    {
        Data = data;
    }

    public T? Data { get; private set; }

    public static Result<T> Success(T data) => new Result<T>(data);

    public static Result<T> Error(Error error) => new Result<T>(default, false, [error]);

    public static Result<T> Error(IEnumerable<Error> errors) => new Result<T>(default, false, errors);

    public static implicit operator Result<T>(T data) => Success(data);

    public static implicit operator Result<T>(Error error) => Error(error);
}

public sealed record Error(string Code, string Message);