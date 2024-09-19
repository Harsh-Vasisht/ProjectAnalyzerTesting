using System.Net;
using BackEndAPI.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace BackEndAPI.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(
            HttpContext context,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            _logger.LogError($"An error occurred while processing your request: {exception.Message}");
            
            var errorResponse = new ErrorResponse
            {
                Message = exception.Message
            };

            switch(exception)
            {
                case BadHttpRequestException:
                    errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Title = exception.GetType().Name;
                    break;
                 
                default:
                    errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Title = "Internal Server Error";
                    break;
            }

            context.Response.StatusCode = errorResponse.StatusCode;

            await context.Response.WriteAsJsonAsync(exception, cancellationToken);

            return true;
        }
    }
}