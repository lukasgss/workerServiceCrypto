using System.Net;
using System.Text.Json;
using Application.Common.Exceptions;

namespace Api.Middlewares;

public sealed class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode statusCode;
        string message;

        switch (exception)
        {
            case BaseApplicationException ex:
                statusCode = ex.HttpStatusCode;
                message = ex.Message;
                break;
            default:
                statusCode = HttpStatusCode.InternalServerError;
                message = "Erro interno no sistema, tente novamente mais tarde.";
                break;
        }

        _logger.LogError("{Exception}", exception);
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsync(JsonSerializer.Serialize(new { statusCode, message }));
    }
}