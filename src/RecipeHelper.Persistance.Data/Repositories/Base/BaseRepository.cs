using Microsoft.EntityFrameworkCore;
using RecipeHelper.Application.Common.Contracts.Persistance;
using RecipeHelper.Persistance.Data.Context;

namespace RecipeHelper.Persistance.Data.Repositories.Base
{
    public class BaseRepository<TEntity, TId> : IAsyncRepository<TEntity, TId> where TEntity : class
    {
        protected readonly RecipeHelperDbContext _dbContext;

        public BaseRepository(RecipeHelperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual IQueryable<TEntity> Entity => _dbContext.Set<TEntity>();

        public virtual async Task<TEntity?> GetByIdAsync(TId id) 
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
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<int> CountTotalAsync()
        {
            return await EntityFrameworkQueryableExtensions.CountAsync(_dbContext.Set<TEntity>());
        }

        public virtual async Task UpdateAync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
