namespace RecipeHelper.Application.Features.Identity.Requests.Auth
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
