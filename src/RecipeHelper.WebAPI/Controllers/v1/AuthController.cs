using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeHelper.Application.Common.Contracts.Interfaces.Auth;
using RecipeHelper.Application.Common.Requests.Auth;
using RecipeHelper.Application.Common.Responses.Identity;
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
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginAsync([FromBody] AuthRequest request)
        {
            var authResponse = (AuthResponse) await _authService.AuthenticateAsync(request);

            if (authResponse != null && !authResponse.Success)
                return BadRequest(authResponse);

            return Ok(authResponse);
        }

        [HttpPost(ApiRoutes.Auth.Register)]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] AuthRegisterRequest request)
        {
            var authResponse = (AuthResponse) await _authService.RegisterAsync(request);

            if (authResponse != null && !authResponse.Success)
                return BadRequest(authResponse);

            return Ok(authResponse);
        }

        [HttpPost(ApiRoutes.Auth.Refresh)]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var authResponse = (AuthResponse) await _authService.RefreshTokenAsync(request);

            if (authResponse != null && !authResponse.Success)
            {
                return BadRequest(new AuthResponse
                {
                    Success = false,
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthResponse
            {
                Success = true,
                Token = authResponse!.Token,
                RefreshToken = authResponse!.RefreshToken
            });
        }

        [HttpPost, Authorize]
        [Route("revoke")]
        public async Task<IActionResult> Revoke()
        {
            var response = (RefreshTokenRevokeResponse) await _authService.RevokeRefreshTokenAsync();

            return Ok(response);
        }
    }
}
