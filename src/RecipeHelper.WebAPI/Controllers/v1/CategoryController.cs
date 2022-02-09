using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.QueryParameters;
using RecipeHelper.Application.Common.Responses;
using RecipeHelper.Application.Features.Categories.Commands.Create;
using RecipeHelper.Application.Features.Categories.Queries.GetCategoriesList;
using RecipeHelper.Application.Features.Categories.Queries.GetCategoryDetails;
using RecipeHelper.WebAPI.Helpers;
using RecipeHelper.WebAPI.Routes.v1;

namespace RecipeHelper.WebAPI.Controllers.v1
{
    [Route("")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.Category.Get)]
        [ProducesResponseType(typeof(Response<CategoryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetCategoryByIdQuery { Id = id });

            return response.Succeeded ?
                Ok(response) :
                BadRequest(response);
        }

        [HttpGet(ApiRoutes.Category.GetAll)]
        [ProducesResponseType(typeof(Response<PaginatedList<CategoryDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] QueryParameters parameters)
        {
            var response = await _mediator.Send(new GetCategoryListQuery { QueryParameters = parameters });

            return response.Succeeded == true ?
                Ok(response) :
                BadRequest(response);
        }

        [HttpPost(ApiRoutes.Category.Create)]
        [ProducesResponseType(typeof(Response<CategoryDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryCommand command)
        {
            var response = await _mediator.Send(command);

            return response.Succeeded ?
                Created(GetLocationUriHelper.GetLocationUri(HttpContext, ApiRoutes.Category.Get, "{id}", response.Data.Id.ToString()), response) :
                BadRequest(response);
        }

        [HttpPut(ApiRoutes.Category.Update)]
        [ProducesResponseType(typeof(Response<CategoryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateCategoryCommand command)
        {
            var response = await _mediator.Send(command);

            return response.Succeeded ?
                Ok(response) :
                BadRequest(response);
        }

        [HttpDelete(ApiRoutes.Category.Delete)]
        [ProducesResponseType(typeof(Response<CategoryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new DeleteCategoryCommand { Id = id });

            return response.Succeeded ?
                Ok(response) :
                BadRequest(response);
        }

    }
}
