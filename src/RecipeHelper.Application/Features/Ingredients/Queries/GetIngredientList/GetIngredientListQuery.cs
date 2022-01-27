using MediatR;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Features.Ingredients.Queries.GetIngredientList
{
    public class GetIngredientListQuery : IRequest<Response<PaginatedList<IngredientDto>>>
    {
        public IngredientQueryParameters QueryParameters { get; set; }
    }
}
