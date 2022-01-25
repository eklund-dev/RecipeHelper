namespace RecipeHelper.Application.Common.Requests.Auth
{
    public class AuthRegisterRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string UserName { get; set; } = null!;
    }
}
