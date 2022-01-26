using MediatR;
using RecipeHelper.Application.Common.Dtos.Recipes;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Features.Recipes.Queries.GetRecipeDetails
{
    public class GetRecipeDetailsByIdQuery : IRequest<Response<RecipeQueryDto>>
    {
        public string Id { get; set; }
    }
}
