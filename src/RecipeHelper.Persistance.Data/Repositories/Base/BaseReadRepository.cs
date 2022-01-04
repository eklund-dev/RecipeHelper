using Microsoft.EntityFrameworkCore;
using RecipeHelper.Application.Common.Contracts.Persistance;
using RecipeHelper.Persistance.Data.Context;

namespace RecipeHelper.Persistance.Data.Repositories.Base
{
    public class BaseReadRepository<TEntity, TId> : IAsyncReadRepository<TEntity, TId> where TEntity : class
    {
        private readonly RecipeHelperDbContext _dbContext;

        public BaseReadRepository(RecipeHelperDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual async Task<TEntity?> GetByIdAsync(TId id) 
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IReadOnlyList<TEntity>> ListAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }
    
    }
}
