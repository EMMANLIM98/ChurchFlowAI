namespace ChurchFlow.Application.Common;

public class Result<T>
{
    public bool IsSuccess { get; }

    public string Error { get; }

    public bool IsFailure => !IsSuccess;

    private Result(bool isSuccess, string error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success()
        => new Result(true, string.Empty);

    public static Result Failure(string error)
        => new Result(false, error);
}