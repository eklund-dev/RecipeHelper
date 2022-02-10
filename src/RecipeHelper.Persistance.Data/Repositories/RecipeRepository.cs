using Microsoft.EntityFrameworkCore;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Domain.Entities;
using RecipeHelper.Persistance.Data.Context;
using RecipeHelper.Persistance.Data.Repositories.Base;

namespace RecipeHelper.Persistance.Data.Repositories
{
    public class RecipeRepository : BaseRepository<Recipe, Guid>, IRecipeRepository
    {
        public RecipeRepository(RecipeHelperDbContext dbContext) : base(dbContext) { }

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

        public override async Task UpdateAync(Recipe entity)
        {
            var existingRecipe = _dbContext.Recipes?
                .Where(r => r.Id == entity.Id)
                .Include(r => r.RecipeCategories)
                .Include(r => r.RecipeIngredients)
                .FirstOrDefault();

            if (existingRecipe != null)
            {
                _dbContext.Entry(existingRecipe).CurrentValues.SetValues(entity);

                if (entity.RecipeCategories != null)
                {
                    // Delete children
                    if (existingRecipe.RecipeCategories != null)
                    {
                        foreach (var rc in existingRecipe.RecipeCategories.ToList())
                        {
                            if (!entity.RecipeCategories.Any(x => x.RecipeId == rc.RecipeId))
                                _dbContext?.RecipeCategories.Remove(rc);
                        }
                    }
                  
                    // Update and insert children
                    foreach (var updatedCategory in entity.RecipeCategories)
                    {
                        var existingCategory = await _dbContext.RecipeCategories
                            .AsNoTracking()
                            .Where(x => x.CategoryId == updatedCategory.CategoryId)
                            .SingleOrDefaultAsync();

                        if (existingCategory != null)
                            _dbContext.Entry(existingCategory).CurrentValues.SetValues(updatedCategory);
                        else
                        {
                            existingRecipe.RecipeCategories?.Add(updatedCategory);
                        }
                    }
                }

                if (entity.RecipeIngredients != null)
                {
                    if (existingRecipe.RecipeIngredients != null)
                    {
                        foreach (var item in existingRecipe.RecipeIngredients)
                        {
                            if (!entity.RecipeIngredients.Any(x => x.IngredientId == item.IngredientId))
                                _dbContext.RecipeIngredients.Remove(item);
                        }
                    }

                    //var existingIngredients = await _dbContext.RecipeIngredients.Where(x => x.RecipeId == entity.Id).ToListAsync();

                    foreach (var updatedIngredient in entity.RecipeIngredients)
                    {
                        var existingIngredient = await _dbContext.RecipeIngredients
                            .AsNoTracking()
                            .Where(x => x.RecipeId == existingRecipe.Id && x.IngredientId == updatedIngredient.Id)
                            .SingleOrDefaultAsync();

                        if (existingIngredient != null)
                        {
                            _dbContext.Entry(existingIngredient).CurrentValues.SetValues(updatedIngredient);
                        }
                        else
                        {
                            var newRi = new RecipeIngredient
                            {
                                Id = Guid.NewGuid(),
                                RecipeId = existingRecipe.Id,
                                IngredientId = updatedIngredient.Id,
                                NumberOfPortionsBase = updatedIngredient.NumberOfPortionsBase,
                                IngredientAmountBase = updatedIngredient.IngredientAmountBase,
                            };

                            existingRecipe.RecipeIngredients?.Add(newRi);
                        }
                    }
                }

                await _dbContext.SaveChangesAsync();

            }
        }

    }
}
