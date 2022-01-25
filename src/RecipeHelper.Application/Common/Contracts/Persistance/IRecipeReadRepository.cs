using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Common.Contracts.Persistance
{
    public interface IRecipeReadRepository : IAsyncReadRepository<Recipe, Guid>
    {
    }
}
