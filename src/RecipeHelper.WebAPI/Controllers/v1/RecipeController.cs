using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeHelper.Application.Common.Dtos.Recipes;
using RecipeHelper.Application.Common.QueryParameters;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Application.Features.Recipes.Queries.GetRecipeDetails;
using RecipeHelper.Application.Features.Recipes.Queries.GetRecipeList;
using RecipeHelper.WebAPI.Routes.v1;

namespace RecipeHelper.WebAPI.Controllers.v1
{
    [Route("")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecipeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.Recipe.Get)]
        [ProducesResponseType(typeof(Response<RecipeQueryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetSingleRecipeAsync([FromRoute] string id)
        {
            var response = await _mediator.Send(new GetRecipeDetailsByIdQuery { Id = id });

            return response.Succeeded == true ? Ok(response) : BadRequest(response);
        }

        [HttpGet(ApiRoutes.Recipe.GetAll)]
        [Authorize(Policy = "Administrator")]
        [ProducesResponseType(typeof(Response<PaginatedList<RecipeQueryDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllRecipesAsync([FromQuery] RecipeQueryParameters parameters)
        {
            var response = await _mediator.Send(new GetRecipeListQuery { Parameters = parameters });

            return response.Succeeded == true ? Ok(response) : BadRequest(response);
        }
    }
}
