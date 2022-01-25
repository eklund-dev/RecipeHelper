using RecipeHelper.Application.Common.Requests.Auth;
using RecipeHelper.Application.Common.Responses.Identity;

namespace RecipeHelper.Application.Common.Contracts.Interfaces.Auth
{
    public interface IAuthService 
    {
        Task<AuthResponse> AuthenticateAsync(AuthRequest request);
        Task<AuthResponse> RegisterAsync(AuthRegisterRequest request);
        Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request);
        Task<RefreshTokenRevokeResponse> RevokeRefreshTokenAsync();
    }
}
