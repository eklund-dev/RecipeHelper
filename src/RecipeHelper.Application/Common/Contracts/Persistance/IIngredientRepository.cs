using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Common.Contracts.Persistance
{
    public interface IIngredientRepository : IAsyncRepository<Ingredient, Guid>
    {
        Task<bool> IsIngredientNameUnique(string name);
    }
}
