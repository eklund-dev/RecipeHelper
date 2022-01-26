using Microsoft.EntityFrameworkCore;
using RecipeHelper.Application.Common.Contracts.Persistance;
using RecipeHelper.Persistance.Data.Context;

namespace RecipeHelper.Persistance.Data.Repositories.Base
{
    public class BaseReadRepository<TEntity> : IAsyncReadRepository<TEntity> where TEntity : class
    {
        protected readonly RecipeHelperDbContext _dbContext;

        public BaseReadRepository(RecipeHelperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual IQueryable<TEntity> Entity => _dbContext.Set<TEntity>();

        public virtual async Task<TEntity?> GetByIdAsync(string id) 
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IReadOnlyList<TEntity>> ListAllAsync()
        {
            return await EntityFrameworkQueryableExtensions.ToListAsync(_dbContext.Set<TEntity>());
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            return entity;
        }
        public virtual async Task UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).CurrentValues.SetValues(entity);
            await Task.CompletedTask;
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await Task.CompletedTask;
        }

        public virtual async Task<int> CountTotalAsync()
        {
            return await EntityFrameworkQueryableExtensions.CountAsync(_dbContext.Set<TEntity>());
        }

        public virtual async Task UpdateAync(TEntity entity)
        {
            _dbContext.Entry(entity).CurrentValues.SetValues(entity);
            await Task.CompletedTask;
        }
    }
}
