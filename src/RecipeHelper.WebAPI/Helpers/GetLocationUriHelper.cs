namespace RecipeHelper.WebAPI.Helpers
{
    public static class GetLocationUriHelper
    {
        public static string GetLocationUri(HttpContext context, string getRoute, string replaceWith, string replaceTo)
        {

            var baseUrl = $"{context.Request.Scheme}://{context.Request.Host.ToUriComponent()}";

            return $"{baseUrl}/{getRoute.Replace(replaceWith, replaceTo)}";
        }
    }
}
