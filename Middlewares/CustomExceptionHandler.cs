using Microsoft.AspNetCore.Mvc;

namespace NZWalksAPI.Middlewares
{
    public class CustomExceptionHandler(
        RequestDelegate next,
        ILogger<CustomExceptionHandler> logger
    )
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<CustomExceptionHandler> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Server Error"
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
