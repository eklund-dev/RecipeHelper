using MediatR;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.QueryParameters;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Features.Recipes.Queries.GetRecipeList
{
    public class GetRecipeListQuery : IRequest<Response<PaginatedList<RecipeDto>>>
    {
        public QueryParameters QueryParameters {get; set;}
    }
}
