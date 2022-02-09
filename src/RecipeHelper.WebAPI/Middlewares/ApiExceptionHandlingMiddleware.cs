using RecipeHelper.Application.Common.Models;
using RecipeHelper.Domain.Exceptions;
using System.Net;

namespace RecipeHelper.WebAPI.Middlewares
{
    public class ApiExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionHandlingMiddleware> _logger;

        public ApiExceptionHandlingMiddleware(RequestDelegate next, ILogger<ApiExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApiException apiEx)
            {
                _logger.LogError($"A new violation exception has been thrown {apiEx}");
                await HandleExceptionsAsync(context, apiEx);
            }
            catch (AccessViolationException avEx)
            {
                _logger.LogError($"A new violation exception has been thrown {avEx}");
                await HandleExceptionsAsync(context, avEx);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionsAsync(context, ex);
            }
        }

        private async static Task HandleExceptionsAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var message = exception switch
            {
                ApiException => $"'Api Exception Error' from the custom middleware. {exception.Message}",
                AccessViolationException => $"'Access Violation Error' from the custom middleware. {exception.Message}",
                _ => $"Internal Server Error from the custom middleware. {exception.Message}"
            };

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
