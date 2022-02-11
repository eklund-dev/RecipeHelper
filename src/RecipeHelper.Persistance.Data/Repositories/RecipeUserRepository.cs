using Microsoft.EntityFrameworkCore;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Domain.Entities;
using RecipeHelper.Persistance.Data.Context;
using RecipeHelper.Persistance.Data.Repositories.Base;

namespace RecipeHelper.Persistance.Data.Repositories
{
    public class RecipeUserRepository : BaseRepository<RecipeUser, Guid>, IRecipeUserRepository
    {
        public RecipeUserRepository(RecipeHelperDbContext dbContext) : base(dbContext) { }
        public override async Task<IReadOnlyList<RecipeUser>> ListAllAsync()
        {
            return await _dbContext.RecipeUsers!
                .Include(x => x.FavoriteRecipes)
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task UpdateAync(RecipeUser entity)
        {
            var existingUser = await _dbContext.RecipeUsers!
                .Where(x => x.Id == entity.Id)
                .Include(x => x.FavoriteRecipes)
                .FirstOrDefaultAsync();
            
            if (existingUser != null)
            {
                _dbContext.Entry(existingUser).CurrentValues.SetValues(entity);

                if (entity.FavoriteRecipes != null)
                {
                    //Delete possible children
                    if (existingUser.FavoriteRecipes != null)
                    {
                        foreach (var fr in existingUser.FavoriteRecipes.ToList())
                        {
                            if (!entity.FavoriteRecipes.Any(x => x.RecipeUserId == fr.RecipeUserId))
                                _dbContext.FavoriteRecipes?.Remove(fr);
                        }
                    }

                    //Update or add new children
                    foreach (var recipe in entity.FavoriteRecipes)
                    {
                        var existingRecipe = await _dbContext.FavoriteRecipes!
                            .Where(x => x.RecipeId == recipe.RecipeId)
                            .AsNoTracking()
                            .SingleOrDefaultAsync();

                        if (existingRecipe != null)
                            _dbContext.Entry(existingRecipe).CurrentValues.SetValues(existingRecipe);
                        else
                        {
                            existingUser.FavoriteRecipes ??= new List<FavoriteRecipe>();
                            existingUser.FavoriteRecipes.Add(recipe);
                        }
                    }
                }

                await _dbContext.SaveChangesAsync();
            }

        }
    }
}
