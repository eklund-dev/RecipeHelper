using MediatR;
using RecipeHelper.Application.Recipes.Responses;

namespace RecipeHelper.Application.Recipe.Queries.GetRecipeDetails
{
    public class GetRecipeDetailsByIdQuery : IRequest<RecipeQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
