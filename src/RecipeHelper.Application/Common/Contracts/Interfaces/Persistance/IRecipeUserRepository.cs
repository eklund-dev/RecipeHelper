using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Common.Contracts.Interfaces.Persistance
{
    public interface IRecipeUserRepository : IAsyncRepository<RecipeUser, Guid>
    {

    }
}
