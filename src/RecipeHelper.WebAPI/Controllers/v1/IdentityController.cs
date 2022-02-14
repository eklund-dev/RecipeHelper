using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeHelper.Application.Common.Contracts.Interfaces.Identity;
using RecipeHelper.Application.Common.Contracts.Interfaces.Identity.Roles;
using RecipeHelper.Application.Common.Dtos.Identity;
using RecipeHelper.Application.Common.QueryParameters;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Application.Features.Identity.Requests.Roles;
using RecipeHelper.Application.Features.Identity.Requests.Users;
using RecipeHelper.WebAPI.Helpers;
using RecipeHelper.WebAPI.Routes.v1;

namespace RecipeHelper.WebAPI.Controllers.v1
{
    [Route("")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
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
        [ProducesResponseType(typeof(Response<ApplicationUserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserAsync([FromRoute] string id)
        {
            var response = await _identityUserService.GetUserByIdAsync(id);
            
            return response.Succeeded ? 
                Ok(response) : 
                BadRequest(response);
        }

        [HttpGet(ApiRoutes.Identity.GetAllUsers)]
        [ProducesResponseType(typeof(Response<PaginatedList<ApplicationUserDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsersAsync([FromQuery] QueryParameters parameters)
        {
            var response = await _identityUserService.GetPaginatedUsersAsync(parameters.PageNumber, parameters.PageSize);

            return response.Succeeded ? 
                Ok(response) : 
                BadRequest(response);
        }

        [HttpPost(ApiRoutes.Identity.CreateUser)]
        [ProducesResponseType(typeof(Response<ApplicationUserDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request)
        {
            var response = await _identityUserService.CreateUserAsync(request);

            return response.Succeeded ?
                Created(GetLocationUriHelper.GetLocationUri(HttpContext, ApiRoutes.Identity.GetUser, "{id}", response.Data.Id.ToString()), response) :
                BadRequest(response);
        }
        
        [HttpPut(ApiRoutes.Identity.UpdateUser)]
        [ProducesResponseType(typeof(Response<ApplicationUserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserRequest request)
        {
            var response = await _identityUserService.UpdateUserAsync(request);

            return response.Succeeded ? 
                Ok(response) : 
                BadRequest(response);
        }

        [HttpDelete(ApiRoutes.Identity.DeleteUser)]
        [ProducesResponseType(typeof(Response<ApplicationUserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUserAsyn([FromRoute] string id)
        {
            var response = await _identityUserService.DeleteUserAsync(id);

            return response.Succeeded ? 
                Ok(response) : 
                BadRequest(response);
        }

        #endregion

        #region Roles

        [HttpGet(ApiRoutes.Identity.GetRole)]
        [ProducesResponseType(typeof(Response<ApplicationRoleDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoleAsync([FromRoute] string id)
        {
            return Ok(await _identityRoleService.GetByIdAsync(id));
        }

        [HttpGet(ApiRoutes.Identity.GetAllRoles)]
        [ProducesResponseType(typeof(Response<ApplicationRoleDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRolesAsync([FromQuery] QueryParameters parameters)
        {
            var roleListResponse = await _identityRoleService.GetAllAsync(parameters.PageNumber, parameters.PageSize);

            return Ok(roleListResponse);
        }

        [HttpPut(ApiRoutes.Identity.UpdateRole)]
        [ProducesResponseType(typeof(Response<ApplicationRoleDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateRoleAsync([FromBody] UpdateRoleRequest request)
        {
            var response = await _identityRoleService.UpdateAsync(request);

            return response.Succeeded ?
                Ok(response) :
                BadRequest(response);
        }

        [HttpPost(ApiRoutes.Identity.CreateRole)]
        [ProducesResponseType(typeof(Response<ApplicationRoleDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateRoleAsync([FromBody] CreateRoleRequest request)
        {
            var response = await _identityRoleService.CreateAsync(request);

            return response.Succeeded ?
                Ok(response) :
                BadRequest(response);
        }

        [HttpDelete(ApiRoutes.Identity.DeleteRole)]
        [ProducesResponseType(typeof(Response<ApplicationRoleDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteRoleAsync([FromRoute] string id)
        {
            var response = await _identityRoleService.DeleteAsync(id);

            return response.Succeeded ?
                Ok(response) : 
                BadRequest(response);
        }
        #endregion

    }
}
