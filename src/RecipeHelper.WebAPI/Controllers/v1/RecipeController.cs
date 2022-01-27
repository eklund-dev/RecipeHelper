using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Application.Features.Recipes;
using RecipeHelper.Application.Features.Recipes.Queries.GetRecipeDetails;
using RecipeHelper.Application.Features.Recipes.Queries.GetRecipeList;
using RecipeHelper.Domain.Exceptions;
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
        [ProducesResponseType(typeof(Response<RecipeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetSingleRecipeAsync([FromRoute] Guid id)
        {
            try
            {
                var response = await _mediator.Send(new GetRecipeDetailsByIdQuery { Id = id });
                return response.Succeeded == true ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }

        }

        [HttpGet(ApiRoutes.Recipe.GetAll)]
        [Authorize(Policy = "Administrator")]
        [ProducesResponseType(typeof(Response<PaginatedList<RecipeDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllRecipesAsync([FromQuery] RecipeQueryParameters parameters)
        {
            var response = await _mediator.Send(new GetRecipeListQuery { QueryParameters = parameters });

            return response.Succeeded == true ? Ok(response) : BadRequest(response);
        }
    }
}
