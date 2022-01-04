namespace RecipeHelper.WebAPI.Routes.v1
{
    public static class ApiRoutes
    {
        public const string ApiVersion1 = "api/v1/";

        public class Authenticate
        {
            private const string Base = ApiVersion1 + "authenticate/";

            public const string Login = Base + "login";
            public const string Register = Base + "register";
            public const string Refresh = Base + "refresh";
        }

        public class Recipe
        {
            private const string Base = ApiVersion1 + "recipe/";

            public const string Get = Base + "{id}";
            public const string GetAll = Base;
            public const string Update = Base;
            public const string Add = Base;
            public const string Delete = "{id}";
        }
    }
}
