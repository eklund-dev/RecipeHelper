using MediatR;
using RecipeHelper.Application.Recipes.Responses;

namespace RecipeHelper.Application.Features.Recipes.Queries.GetRecipeList
{
    public class GetRecipeListQuery : IRequest<RecipeQueryAllResponse>
    {

    }
}
