using System.Text.Json;
using Receivables.DTO.Outgoing;

namespace API.Services.Errors;

public class ExceptionHandlingMiddleware(RequestDelegate next, IWebHostEnvironment env)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        switch (exception)
        {
            case RecordNotFoundException:
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;

                var result = JsonSerializer.Serialize(new Response(false, exception.Message ));
                return context.Response.WriteAsync(result);
            }

            case BadRequestException:
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var result = JsonSerializer.Serialize(new Response(false, exception.Message ));
                return context.Response.WriteAsync(result);
            }
        }

        if (env.IsDevelopment())
        {
            var error = exception.Message;
            var stackTrace = exception.StackTrace?.Trim();

            if (exception.InnerException != null)
            {
                error = exception.InnerException.Message;
                stackTrace = exception.InnerException.StackTrace?.Trim();
            }

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var devResult = JsonSerializer.Serialize(new { Success = false, Error = error, StackTrace = stackTrace });
            return context.Response.WriteAsync(devResult);
        }

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var prodResult = JsonSerializer.Serialize(new Response(false, "Internal Server Error"));
        return context.Response.WriteAsync(prodResult);
    }
}