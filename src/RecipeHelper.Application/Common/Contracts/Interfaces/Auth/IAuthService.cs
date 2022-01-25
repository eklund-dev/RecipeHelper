using RecipeHelper.Application.Common.Dtos.Identity;
using RecipeHelper.Application.Common.Requests.Auth;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Common.Contracts.Interfaces.Auth
{
    public interface IAuthService 
    {
        Task<Response<AuthDto>> AuthenticateAsync(AuthRequest request);
        Task<Response<AuthDto>> RegisterAsync(AuthRegisterRequest request);
        Task<Response<AuthDto>> RefreshTokenAsync(RefreshTokenRequest request);
        Task<Response<RefreshTokenRevokeDto>> RevokeRefreshTokenAsync();
    }
}
