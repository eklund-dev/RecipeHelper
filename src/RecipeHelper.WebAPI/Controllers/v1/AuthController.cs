using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeHelper.Application.Common.Contracts.Interfaces.Auth;
using RecipeHelper.Application.Common.Dtos.Identity;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Application.Features.Identity.Requests.Auth;
using RecipeHelper.WebAPI.Routes.v1;

namespace RecipeHelper.WebAPI.Controllers.v1
{
    [ApiController]
    [Route("")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost(ApiRoutes.Auth.Login)]
        [ProducesResponseType(typeof(Response<AuthDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginAsync([FromBody] AuthRequest request)
        {
            var response = await _authService.AuthenticateAsync(request);

            return response.Succeeded ?
                Ok(response) :
                BadRequest(response);
        }

        [HttpPost(ApiRoutes.Auth.Register)]
        [ProducesResponseType(typeof(Response<AuthDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] AuthRegisterRequest request)
        {
            var response = await _authService.RegisterAsync(request);

            return response.Succeeded ?
                Ok(response) :
                BadRequest(response);
        }

        [HttpPost(ApiRoutes.Auth.Refresh)]
        [ProducesResponseType(typeof(Response<AuthDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var response = await _authService.RefreshTokenAsync(request);

            return response.Succeeded ?
                Ok(response) :
                BadRequest(response);
        }

        [HttpPost, Authorize]
        [ProducesResponseType(typeof(Response<RefreshTokenRevokeDto>), StatusCodes.Status200OK)]
        [Route("revoke")]
        public async Task<IActionResult> Revoke()
        {
            var response = await _authService.RevokeRefreshTokenAsync();

            return response.Succeeded ?
                Ok(response) :
                BadRequest(response);
        }
    }
}
