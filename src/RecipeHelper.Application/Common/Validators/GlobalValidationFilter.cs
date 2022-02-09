using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RecipeHelper.Application.Common.Responses;
using System.Text.Json;

namespace RecipeHelper.Application.Common.Validators
{
    public class GlobalValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Before controller runs

            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors
                    .Select(x => x.ErrorMessage))
                    .ToArray();

                var errorModel = new GlobalFilterError();

                foreach (var error in errorsInModelState)
                {
                    foreach (var subError in error.Value)
                    {
                        errorModel.Errors.Add(new ValidationFilterError { FieldName = error.Key, Message = subError });
                    }
                }

                context.Result = new BadRequestObjectResult(
                    Response<GlobalFilterError>.Fail(
                        "Error from validtaion filter", 
                        JsonSerializer.Serialize(errorModel.Errors).Split(',').ToList()));
            }

            //After Controller is done
            await next();

        }
    }
}
