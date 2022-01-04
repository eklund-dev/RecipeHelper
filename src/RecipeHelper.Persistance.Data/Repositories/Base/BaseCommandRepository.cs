using Microsoft.EntityFrameworkCore;
using RecipeHelper.Application.Common.Contracts.Persistance;
using RecipeHelper.Persistance.Data.Context;

namespace RecipeHelper.Persistance.Data.Repositories.Base
{
    public class BaseCommandRepository<TEntity> : IAsyncCommandRepository<TEntity> where TEntity : class
    {
        private readonly RecipeHelperDbContext _dbContext;

        public BaseCommandRepository(RecipeHelperDbContext dbContext)
        {
            _dbContext = dbContext;
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

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
