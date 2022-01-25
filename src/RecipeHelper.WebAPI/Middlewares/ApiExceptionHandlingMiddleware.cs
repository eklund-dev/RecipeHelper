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
            catch (AccessViolationException avEx)
            {
                _logger.LogError($"A new violation exception has been thrown {avEx}");
                await HandleExceptionsAsync(context, avEx);
            }
            catch (ApplicationUserViolationException appUserEx)
            {
                _logger.LogError($"A new violation exception has been thrown {appUserEx}");
                await HandleExceptionsAsync(context, appUserEx);
            }
            catch (ApplicationRoleViolationException roleEx)
            {
                _logger.LogError($"A new violation exception has been thrown {roleEx}");
                await HandleExceptionsAsync(context, roleEx);
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

            // Bara att addera nya exeptions här. Försök att endast använda det inbyggda exceptions som finns - kommer långt med dem. 
            // Exceptions bör endast skapas vid oväntaden händelser - det är dyrt att throwa exceptions.

            var message = exception switch
            {
                AccessViolationException => $"'Access Violation Error' from the custom middleware. {exception.Message}",
                ApplicationUserViolationException => $"'Application User Violation' from the custom middleware. {exception.Message}",
                ApplicationRoleViolationException => $"'Role violation error' from the custom middleware. {exception.Message}",
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
