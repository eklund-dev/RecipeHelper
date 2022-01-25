using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeHelper.Application.Common.Contracts.Interfaces.Identity;
using RecipeHelper.Application.Common.Contracts.Interfaces.Identity.Roles;
using RecipeHelper.Application.Common.Requests.Roles;
using RecipeHelper.Application.Common.Responses.Identity;
using RecipeHelper.WebAPI.Routes.v1;

namespace RecipeHelper.WebAPI.Controllers.v1
{
    [Route("")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityUserService _identityUserService;
        private readonly IIdentityRoleService _identityRoleService;
        public IdentityController(
            IIdentityRoleService identityRoleService, 
            IIdentityUserService identityUserService)
        {
            _identityRoleService = identityRoleService;
            _identityUserService = identityUserService;
        }

        #region Users

        [HttpGet(ApiRoutes.Identity.GetUser)]
        [ProducesResponseType(typeof(ApplicationUserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserAsync([FromRoute] string id)
        {
            var response = await _identityUserService.GetByIdAsync(id);
            
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet(ApiRoutes.Identity.GetAllUsers)]
        [ProducesResponseType(typeof(ApplicationUserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsersAsync()
        {
            var userListResponse = await _identityUserService.GetAllAsync();

            return Ok(userListResponse);
        }

        #endregion

        #region Roles

        [HttpGet(ApiRoutes.Identity.GetRole)]
        [ProducesResponseType(typeof(ApplicationRoleResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoleAsync([FromRoute] string id)
        {
            return Ok(await _identityRoleService.GetByIdAsync(id));
        }

        [HttpGet(ApiRoutes.Identity.GetAllRoles)]
        [ProducesResponseType(typeof(ApplicationRoleResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRolesAsync()
        {
            var roleListResponse = await _identityRoleService.GetAllAsync();

            return Ok(roleListResponse);
        }

        [HttpPut(ApiRoutes.Identity.UpdateRole)]
        [ProducesResponseType(typeof(ApplicationRoleResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateRoleAsync([FromBody] UpdateRoleRequest request)
        {
            var response = await _identityRoleService.UpdateAsync(request);

            return response.Success ?
                Ok(response) :
                BadRequest(response);
        }

        [HttpPost(ApiRoutes.Identity.CreateRole)]
        [ProducesResponseType(typeof(ApplicationRoleResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateRoleAsync([FromBody] CreateRoleRequest request)
        {
            var response = await _identityRoleService.CreateAsync(request);

            return response.Success ?
                Ok(response) :
                BadRequest(response);
        }

        [HttpDelete(ApiRoutes.Identity.DeleteRole)]
        [ProducesResponseType(typeof(ApplicationRoleResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteRoleAsync([FromRoute] string id)
        {
            var response = await _identityRoleService.DeleteAsync(id);

            return response.Success ?
                Ok(response) : 
                BadRequest(response);
        }
        #endregion

    }
}
