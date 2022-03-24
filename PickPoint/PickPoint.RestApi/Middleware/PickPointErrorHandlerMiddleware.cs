using Microsoft.AspNetCore.Http;
using PickPoint.Lib.Domain.Exceptions;

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
            PickPointValidationException => StatusCodes.Status400BadRequest,
            PickPointUnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            PickPointEntityNotFoundException => StatusCodes.Status404NotFound,
            PickPointAccessDeniedException => StatusCodes.Status403Forbidden,
            
            null => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}