using MediatR;
using RecipeHelper.Application.Common.Dtos;
using RecipeHelper.Application.Common.QueryParameters;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Features.Ingredients.Queries.GetIngredientList
{
    public class GetCategoryListQuery : IRequest<Response<PaginatedList<IngredientDto>>>
    {
        public QueryParameters QueryParameters { get; set; }
    }
}
