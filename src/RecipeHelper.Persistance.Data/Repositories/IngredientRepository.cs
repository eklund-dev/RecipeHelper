using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Domain.Entities;
using RecipeHelper.Persistance.Data.Context;
using RecipeHelper.Persistance.Data.Repositories.Base;

namespace RecipeHelper.Persistance.Data.Repositories
{
    public class IngredientRepository : BaseRepository<Ingredient, Guid>, IIngredientRepository
    {
        public IngredientRepository(RecipeHelperDbContext dbContext) : base(dbContext) { }

        public Task<bool> IsIngredientNameUnique(string name, CancellationToken token)
        {
            var matches = _dbContext.Ingredients!.Any(x => x.Name.Equals(name));
            return Task.FromResult(matches);
        }
    }
}
