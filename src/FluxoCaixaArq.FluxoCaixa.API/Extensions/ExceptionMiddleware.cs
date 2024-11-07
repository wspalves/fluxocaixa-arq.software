using System.Net;
using System.Text.Json;
using FluxoCaixaArq.Core.DomainObjects;

namespace FluxoCaixaArq.FluxoCaixa.API.Extensions;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
            Log.Information("Request was processed successfully");
        }
        catch (DomainException ex)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await HandleRequestExceptionAsync(httpContext, ex);
        }
        catch (Exception ex)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await HandleRequestExceptionAsync(httpContext, ex);
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }

    private async Task HandleRequestExceptionAsync(HttpContext context, Exception exception)
    {
        Log.Error(exception, "RequestError");

        var responseObject = new
        {
            message = exception.Message
        };
        context.Response.ContentType = "application/json";
        await JsonSerializer.SerializeAsync(context.Response.Body, responseObject);
    }
}