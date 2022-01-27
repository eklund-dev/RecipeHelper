using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Application.Features.Ingredients;
using RecipeHelper.Application.Features.Ingredients.Commands.Create;
using RecipeHelper.Application.Features.Ingredients.Commands.Delete;
using RecipeHelper.Application.Features.Ingredients.Commands.Update;
using RecipeHelper.Application.Features.Ingredients.Queries.GetIngredientDetails;
using RecipeHelper.Application.Features.Ingredients.Queries.GetIngredientList;
using RecipeHelper.Domain.Exceptions;
using RecipeHelper.WebAPI.Routes.v1;

namespace RecipeHelper.WebAPI.Controllers.v1
{
    [Route("")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IngredientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.Ingredient.Get)]
        [ProducesResponseType(typeof(Response<IngredientDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            try
            {
                var response = await _mediator.Send(new GetIngredientDetailsByIdQuery { Id = id });

                return response.Succeeded ?
                    Ok(response) :
                    BadRequest(response);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }

        [HttpGet(ApiRoutes.Ingredient.GetAll)]
        [ProducesResponseType(typeof(Response<PaginatedList<IngredientDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync([FromQuery] IngredientQueryParameters parameters)
        {
            var response = await _mediator.Send(new GetIngredientListQuery { QueryParameters = parameters });

            return response.Succeeded ?
                Ok(response) :
                BadRequest(response);
        }

        [HttpPost(ApiRoutes.Ingredient.Create)]
        [ProducesResponseType(typeof(Response<IngredientDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateIngredientCommand command)
        {
            var response = await _mediator.Send(command);

            return response.Succeeded ?
                Ok(response) :
                BadRequest(response);
        }

        [HttpPut(ApiRoutes.Ingredient.Update)]
        [ProducesResponseType(typeof(Response<IngredientDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateIngredientCommand command)
        {
            var response = await _mediator.Send(command);

            return response.Succeeded ?
                Ok(response) :
                BadRequest(response);
        }

        [HttpDelete(ApiRoutes.Ingredient.Delete)]
        [ProducesResponseType(typeof(Response<IngredientDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            try
            {
                var response = await _mediator.Send(new DeleteIngredientCommand { Id = id });

                return response.Succeeded ?
                    Ok(response) :
                    BadRequest(response);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        
        }
    }
}
