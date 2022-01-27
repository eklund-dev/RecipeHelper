using RecipeHelper.Application.Common.Contracts.Persistance;
using RecipeHelper.Domain.Entities;
using RecipeHelper.Persistance.Data.Context;
using RecipeHelper.Persistance.Data.Repositories.Base;

namespace RecipeHelper.Persistance.Data.Repositories
{
    public class RecipeRepository : BaseRepository<Recipe, Guid>, IRecipeRepository
    {
        public RecipeRepository(RecipeHelperDbContext dbContext) : base(dbContext) { }

    }
}
