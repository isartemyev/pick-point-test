using PickPoint.Lib.Extensions;

namespace PickPoint.RestApi.Middleware;

public class ErrorDetails
{
    public string Message { get; }

    public ErrorDetails(string message)
    {
        Message = message;
    }

    public override string ToString()
    {
        return this.ToJson();
    }

    public static implicit operator string(ErrorDetails details) => details.ToString();
}