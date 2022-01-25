namespace RecipeHelper.Application.Common.Responses.Identity
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiration { get; set; }

        public bool Success { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
