using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Common.Contracts.Interfaces.Persistance
{
    public interface IRecipeIngredientRepository : IAsyncRepository<RecipeIngredient, Guid>
    {
    }
}
