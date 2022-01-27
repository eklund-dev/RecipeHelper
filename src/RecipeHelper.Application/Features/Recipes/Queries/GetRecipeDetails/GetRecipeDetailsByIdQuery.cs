using MediatR;
using RecipeHelper.Application.Common.Responses;

namespace RecipeHelper.Application.Features.Recipes.Queries.GetRecipeDetails
{
    public class GetRecipeDetailsByIdQuery : IRequest<Response<RecipeDto>>
    {
        public Guid Id { get; set; }
    }
}
