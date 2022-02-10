using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Common.Contracts.Interfaces.Persistance
{
    public interface IFoodTypeRepository : IAsyncRepository<FoodType, Guid>
    {
        Task<bool> FoodTypeExistsAsync(Guid id);
    }
}
