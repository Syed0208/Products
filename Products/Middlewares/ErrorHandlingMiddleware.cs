using Domain.Exceptions;
using System.Text.Json;

namespace Api.Middlewares;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException notFound)
        {
            await HandleExceptionAsync(context, 404, notFound.Message);
        }
        catch (StockCannotBeNegativeException negativeStock)
        {
            await HandleExceptionAsync(context, 400, negativeStock.Message);

        }
        catch (Exception)
        {
            await HandleExceptionAsync(context, 500, "Something went wrong.");
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, int statusCode, string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var errorResponse = new
        {
            StatusCode = statusCode,
            Message = message
        };

        var jsonResponse = JsonSerializer.Serialize(errorResponse);
        await context.Response.WriteAsync(jsonResponse);
    }

}
