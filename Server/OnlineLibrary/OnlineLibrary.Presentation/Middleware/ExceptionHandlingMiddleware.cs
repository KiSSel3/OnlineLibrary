using System.Net;
using FluentValidation;
using Newtonsoft.Json;
using OnlineLibrary.BLL.Exceptions;

namespace OnlineLibrary.Presentation.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    
    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var response = context.Response;

        switch (exception)
        {
            case AuthenticationException:
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;
            case EntityAlreadyExistsException:
                response.StatusCode = (int)HttpStatusCode.Conflict;
                break;
            case EntityNotFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            case FormatException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case ValidationException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case UnauthorizedAccessException:
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        var result = JsonConvert.SerializeObject(new 
        {
            error = exception.Message,
            details = exception.StackTrace
        });

        return context.Response.WriteAsync(result);
    }
}