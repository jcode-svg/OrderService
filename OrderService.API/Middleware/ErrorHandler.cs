using Newtonsoft.Json;
using OrderService.SharedKernel;
using System.Net;

namespace OrderService.API.Middleware;

public class ErrorHandler
{
    private readonly RequestDelegate _next;

    public ErrorHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            Console.WriteLine($"Error handler caught exception => {error.StackTrace}");

            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var responseBody = ResponseWrapper<string>.Error("Something went wrong, your request could not be processed.");

            await response.WriteAsync(JsonConvert.SerializeObject(responseBody));
        }
    }
}
