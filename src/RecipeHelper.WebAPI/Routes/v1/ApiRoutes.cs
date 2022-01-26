namespace RecipeHelper.WebAPI.Routes.v1
{
    public static class ApiRoutes
    {
        private const string ApiVersion1 = "recipeApi/v1/";

        public class Auth
        {
            private const string _base = ApiVersion1 + "auth/";

            public const string Login = _base + "login";
            public const string Register = _base + "register";
            public const string Refresh = _base + "refresh";
        }

        public class Identity
        {
            private const string _base = ApiVersion1 + "identity/";

            public const string GetUser = _base + "user/{id}";
            public const string GetAllUsers = _base + "user";
            public const string CreateUser = _base + "user";
            public const string UpdateUser = _base + "user";
            public const string DeleteUser = _base + "user/{id}";

            public const string GetRole = _base + "role/{id}";
            public const string GetAllRoles = _base + "role";
            public const string CreateRole = _base + "role";
            public const string UpdateRole = _base + "role";
            public const string DeleteRole = _base + "role/{id}";
        }

        public class Recipe
        {
            private const string _base = ApiVersion1 + "recipe/";

            public const string Get = _base + "{id}";
            public const string GetAll = _base;
            public const string Update = _base;
            public const string Add = _base;
            public const string Delete = "{id}";
        }
    }
}
