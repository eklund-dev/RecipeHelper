using MediatR;
using RecipeHelper.Application.Common.Dtos.Recipes;
using RecipeHelper.Application.Common.QueryParameters;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Features.Recipes.Queries.GetRecipeList
{
    public class GetRecipeListQuery : IRequest<Response<PaginatedList<RecipeQueryDto>>>
    {
        public RecipeQueryParameters Parameters {get; set;}
    }
}
