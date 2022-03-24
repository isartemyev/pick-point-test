using Microsoft.AspNetCore.Http;

namespace PickPoint.RestApi.Middleware;

public class PickPointErrorHandlerMiddleware : ErrorHandlerMiddleware
{
    public PickPointErrorHandlerMiddleware(RequestDelegate next) : base(next)
    {
    }

    protected override int DefineStatusCode(Exception exception)
    {
        return exception switch
        {

            null => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}