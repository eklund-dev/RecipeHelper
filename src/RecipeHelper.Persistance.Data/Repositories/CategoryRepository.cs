using Microsoft.EntityFrameworkCore;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Domain.Entities;
using RecipeHelper.Persistance.Data.Context;
using RecipeHelper.Persistance.Data.Repositories.Base;

namespace RecipeHelper.Persistance.Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category, Guid>, ICategoryRepository
    {
        public CategoryRepository(RecipeHelperDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Name.Equals(name));
        }

        public Task<bool> IsCategoryNameUnique(string name, CancellationToken token)
        {
            var matches = _dbContext.Categories.Any(x => x.Name.Equals(name));
            return Task.FromResult(matches);
        }

    }
}
