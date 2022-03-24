using Microsoft.AspNetCore.Http;
using Serilog;

namespace PickPoint.RestApi.Middleware;

public abstract class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            Log.Error(exception, "{Message}", exception.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode  = DefineStatusCode(exception);
            
            var text = new ErrorDetails(exception.Message);
            await context.Response.WriteAsync(text);
        }
    }

    protected abstract int DefineStatusCode(Exception exception);
}