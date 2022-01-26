using RecipeHelper.Application.Common.Contracts.Persistance;
using RecipeHelper.Domain.Entities;
using RecipeHelper.Persistance.Data.Context;
using RecipeHelper.Persistance.Data.Repositories.Base;

namespace RecipeHelper.Persistance.Data.Repositories.Recipes
{
    public class RecipeReadRepository : BaseReadRepository<Recipe>, IRecipeReadRepository
    {
        public RecipeReadRepository(RecipeHelperDbContext dbContext) : base(dbContext) { }

    }
}
