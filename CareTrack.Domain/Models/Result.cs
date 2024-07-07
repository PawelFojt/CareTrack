namespace CareTrack.Domain.Models;


public class Result<TValue>
{
    public TValue? Value { get; set; }
    public bool IsError { get; set; }
    public string Message { get; set; } = string.Empty;

    public Result()
    {
    }
    public Result(TValue result)
    {
        Value = result;
    }

    public static Result<TValue> Error(string message)
    {
        return new Result<TValue>()
        {
            IsError = true,
            Message = message
        };
    }

    public static Result<TValue> Info(string message)
    {
        return new Result<TValue>()
        {
            Message = message
        };
    }
}