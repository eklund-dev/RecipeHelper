namespace RecipeHelper.Application.Common.Dtos.Identity
{
    public class AuthDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
