using MediatR;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Features.Ingredients.Queries.GetIngredientDetails
{
    public class GetIngredientDetailsByIdQuery : IRequest<Response<IngredientDto>>
    {
        public Guid Id { get; set; }
    }
}
