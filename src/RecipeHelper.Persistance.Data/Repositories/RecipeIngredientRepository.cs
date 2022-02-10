using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Domain.Entities;
using RecipeHelper.Persistance.Data.Context;
using RecipeHelper.Persistance.Data.Repositories.Base;

namespace RecipeHelper.Persistance.Data.Repositories
{
    public class RecipeIngredientRepository : BaseRepository<RecipeIngredient, Guid>, IRecipeIngredientRepository
    {
        public RecipeIngredientRepository(RecipeHelperDbContext dbContext) : base(dbContext)
        {
        }

    }
}
