using MediatR;
using RecipeHelper.Application.Recipes.Responses;

namespace RecipeHelper.Application.Features.Recipes.Queries.GetRecipeDetails
{
    public class GetRecipeDetailsByIdQuery : IRequest<RecipeQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
