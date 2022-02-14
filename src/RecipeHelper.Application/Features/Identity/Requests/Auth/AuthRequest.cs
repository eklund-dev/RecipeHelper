namespace RecipeHelper.Application.Features.Identity.Requests.Auth
{
    public class AuthRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
