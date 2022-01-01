namespace RecipeHelper.Application.Common.Contracts
{
    public interface IAsyncCommandRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
