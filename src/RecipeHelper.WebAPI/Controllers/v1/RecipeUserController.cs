using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.QueryParameters;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Application.Features.RecipeUsers.Commands.Create;
using RecipeHelper.Application.Features.RecipeUsers.Queries.GetRecipeUserDetails;
using RecipeHelper.Application.Features.RecipeUsers.Queries.GetRecipeUserList;
using RecipeHelper.WebAPI.Routes.v1;

namespace RecipeHelper.WebAPI.Controllers.v1
{
    [Route("")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class RecipeUserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecipeUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.RecipeUser.Get)]
        [ProducesResponseType(typeof(Response<RecipeUserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecipeUserAsync([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetRecipeUserDetailsByIdQuery { Id = id });
            return response.Succeeded
                ? Ok(response)
                : BadRequest(response);
        }

        [HttpGet(ApiRoutes.RecipeUser.GetAll)]
        [ProducesResponseType(typeof(Response<PaginatedList<RecipeUserDto>>), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetRecipeUserListAsync([FromRoute] QueryParameters parameters)
        {
            var response = await _mediator.Send(new GetRecipeUserListQuery { QueryParameters = parameters });

            return response.Succeeded
                ? Ok(response)
                : BadRequest(response);
        }

        [HttpPost(ApiRoutes.RecipeUser.Create)]
        [ProducesResponseType(typeof(Response<RecipeUserDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateRecipeUserAsync([FromBody] CreateRecipeUserCommand cmd)
        {
            var response = await _mediator.Send(cmd);
            
            return response.Succeeded 
                ? Ok(response) 
                : BadRequest(response);
        }
    }
}
