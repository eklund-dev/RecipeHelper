using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Common.Contracts.Persistance
{
    public interface IRecipeRepository : IAsyncRepository<Recipe, Guid>
    {
    }
}
