using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeHelper.Application.Features.Recipes.Queries.GetRecipeDetails;
using RecipeHelper.Application.Features.Recipes.Queries.GetRecipeList;
using RecipeHelper.Application.Recipes.Responses;
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
        [ProducesResponseType(typeof(RecipeQueryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetSingleRecipeAsync([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetRecipeDetailsByIdQuery { Id = id });

            return response.Success == true ? Ok(response) : BadRequest(response);
        }

        [HttpGet(ApiRoutes.Recipe.GetAll)]
        [Authorize(Policy = "Administrator")]
        [ProducesResponseType(typeof(RecipeQueryAllResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllRecipesAsync()
        {
            var response = await _mediator.Send(new GetRecipeListQuery());

            return response.Success == true ? Ok(response) : BadRequest(response);
        }
    }
}
