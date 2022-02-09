namespace RecipeHelper.Application.Common.Contracts.Interfaces.Persistance
{
    public interface IAsyncRepository<TEntity, TId> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(TId id);
        Task<IReadOnlyList<TEntity>> ListAllAsync();
        Task UpdateAync(TEntity entity);
        Task<TEntity> AddAsync(TEntity Entity);
        Task DeleteAsync(TEntity entity);
        Task<int> CountTotalAsync();

        /// <summary>
        ///     Iqueryable entity of Entity Framework. Use this to execute query in database level.
        /// </summary>
        IQueryable<TEntity> Entity { get; }
    }
}
