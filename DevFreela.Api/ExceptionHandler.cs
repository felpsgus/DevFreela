using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Api;

public class ExceptionHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = exception.GetType().Name,
            Title = "Server error",
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);

        return new ValueTask<bool>(true);
    }
}