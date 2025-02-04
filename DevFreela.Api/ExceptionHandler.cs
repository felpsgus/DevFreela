using DevFreela.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Api;

public class ExceptionHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = exception switch
        {
            DomainException => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = exception.GetType().Name,
                Title = "Bad request",
                Detail = exception.Message
            },
            _ => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = exception.GetType().Name,
                Title = "Server error",
                Detail = exception.Message
            }
        };

        httpContext.Response.StatusCode = (int)problemDetails.Status;
        httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);

        return new ValueTask<bool>(true);
    }
}