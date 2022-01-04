using MediatR;
using RecipeHelper.Application.Recipes.Responses;

namespace RecipeHelper.Application.Recipe.Queries.GetRecipeDetails
{
    public class GetRecipeDetailsByIdQueryHandler : IRequestHandler<GetRecipeDetailsByIdQuery, RecipeQueryResponse>
    {
      
        public Task<RecipeQueryResponse> Handle(GetRecipeDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
