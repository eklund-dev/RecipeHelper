using RecipeHelper.WebAPI.Middlewares;

namespace RecipeHelper.WebAPI.Extensions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtensisons(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RecipeHelper.WebAPI v1"));
        }

        public static void UserErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApiExceptionHandlingMiddleware>();
        }
    }
}
