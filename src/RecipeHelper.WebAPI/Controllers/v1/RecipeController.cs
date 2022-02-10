using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.QueryParameters;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Application.Features.Recipes.Commands.Create;
using RecipeHelper.Application.Features.Recipes.Commands.Update;
using RecipeHelper.Application.Features.Recipes.Queries.GetRecipeDetails;
using RecipeHelper.Application.Features.Recipes.Queries.GetRecipeList;
using RecipeHelper.WebAPI.Routes.v1;

namespace RecipeHelper.WebAPI.Controllers.v1
{
    [Route("")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecipeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.Recipe.Get)]
        [ProducesResponseType(typeof(Response<RecipeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSingleRecipeAsync([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetRecipeDetailsByIdQuery { Id = id });
            return response.Succeeded == true ? 
                Ok(response) : 
                BadRequest(response);          
        }

        [HttpGet(ApiRoutes.Recipe.GetAll)]
        [ProducesResponseType(typeof(Response<PaginatedList<RecipeDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRecipesAsync([FromQuery] QueryParameters parameters)
        {
            var response = await _mediator.Send(new GetRecipeListQuery { QueryParameters = parameters });

            return response.Succeeded == true ? 
                Ok(response) : 
                BadRequest(response);
        }

        [HttpPost(ApiRoutes.Recipe.Create)]
        [ProducesResponseType(typeof(Response<RecipeDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateRecipeAsync([FromBody] CreateRecipeCommand cmd)
        {
            var response = await _mediator.Send(cmd);
            return response.Succeeded ?
                Ok(response) :
                BadRequest(response);
        }

        [HttpPut(ApiRoutes.Recipe.Update)]
        [ProducesResponseType(typeof(Response<RecipeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateRecipeAsync([FromBody] UpdateRecipeCommand cmd)
        {
            var response = await _mediator.Send(cmd);
            return response.Succeeded ?
                Ok(response) :
                BadRequest(response);
        }
    }
}
