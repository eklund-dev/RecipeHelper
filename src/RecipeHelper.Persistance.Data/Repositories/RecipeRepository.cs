using Microsoft.EntityFrameworkCore;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Domain.Entities;
using RecipeHelper.Persistance.Data.Context;
using RecipeHelper.Persistance.Data.Extensions;
using RecipeHelper.Persistance.Data.Repositories.Base;

namespace RecipeHelper.Persistance.Data.Repositories
{
    public class RecipeRepository : BaseRepository<Recipe, Guid>, IRecipeRepository
    {
        public RecipeRepository(RecipeHelperDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Recipe>> GetFavoriteRecipesForUser(Guid recipeUserId)
        {
            return await _dbContext.FavoriteRecipes
                .Where(x => x.RecipeUserId.Equals(recipeUserId))
                .Select(x => x.Recipe)
                .ToListAsync();
        }

        public Task<bool> IsRecipeNameUnique(string name, CancellationToken token)
        {
            return Task.FromResult(_dbContext.Recipes!.Any(x => x.Name.Equals(name)));
        }

        public override async Task<IReadOnlyList<Recipe>> ListAllAsync()
        {
            return await _dbContext.Recipes!
                .AsNoTracking()
                .Include(x => x.RecipeCategories)
                .Include(x => x.RecipeIngredients)
                .ToListAsync();
        }

        public override async Task UpdateAync(Recipe parentRecipe)
        {
            var originalRecipe = await _dbContext.Recipes
                .Where(r => r.Id == parentRecipe.Id)
                .Include(r => r.RecipeCategories)
                .Include(r => r.RecipeIngredients)
                .SingleOrDefaultAsync();

            _dbContext.Entry(originalRecipe).CurrentValues.SetValues(parentRecipe);

            foreach (var childRc in parentRecipe.RecipeCategories)
            {
                var originalRc = originalRecipe.RecipeCategories
                    .Where(rc => rc.RecipeId == childRc.RecipeId && rc.CategoryId == childRc.CategoryId)
                    .SingleOrDefault();

                // Its original child item with mathing id's
                if (originalRc != null)
                {
                    var childEntry = _dbContext.Entry(originalRc);
                    childEntry.CurrentValues.SetValues(childRc);
                }
                else
                {
                    // No => its a new child items
                    originalRecipe.RecipeCategories.Add(childRc);
                }
            }
            // Iterate through list and delete
            foreach (var originalRc in originalRecipe.RecipeCategories.ToList())
            {
                // Does childitems in original collection exists that are not in the new list? - Delete them
                if (!parentRecipe.RecipeCategories.Any(r => r.RecipeId == originalRecipe.Id))
                    //Yes this one is
                    _dbContext.RecipeCategories.Remove(originalRc);
            }

            // Recipe ingredients(child 2)

            foreach (var childRi in parentRecipe.RecipeIngredients)
            {
                var originalRi = originalRecipe.RecipeIngredients
                    .Where(ri => ri.RecipeId == childRi.RecipeId && ri.IngredientId == childRi.IngredientId)
                    .SingleOrDefault();

                if (originalRi != null)
                {
                    var childEntry = _dbContext.Entry(originalRi);
                    childEntry.CurrentValues.SetValues(childRi);
                }
                else
                {
                    originalRecipe.RecipeIngredients.Add(childRi);
                }
            }

            foreach (var originalRi in originalRecipe.RecipeIngredients.ToList())
            {
                // Does childitems in original collection exists that are not in the new list? - Delete them
                if (!parentRecipe.RecipeIngredients.Any(r => r.RecipeId == originalRecipe.Id))
                    // Yes this one is
                    _dbContext.RecipeIngredients.Remove(originalRi);
            }

            await _dbContext.SaveChangesAsync();

        }
    }
}
