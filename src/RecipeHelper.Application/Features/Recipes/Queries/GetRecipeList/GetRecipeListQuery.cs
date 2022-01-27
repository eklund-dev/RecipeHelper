using MediatR;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Features.Recipes.Queries.GetRecipeList
{
    public class GetRecipeListQuery : IRequest<Response<PaginatedList<RecipeDto>>>
    {
        public RecipeQueryParameters QueryParameters {get; set;}
    }
}
