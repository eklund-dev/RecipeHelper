using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Common.Contracts.Interfaces.Persistance
{
    public interface IRecipeRepository : IAsyncRepository<Recipe, Guid>
    {
        Task<bool> IsRecipeNameUnique(string name, CancellationToken token);
        Task<IEnumerable<Recipe>> GetFavoriteRecipesForUser(Guid recipeUserId);
    }
}
