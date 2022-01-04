namespace RecipeHelper.Application.Common.Contracts.Persistance
{
    public interface IAsyncReadRepository<TEntity, TId> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(TId id);
        Task<IReadOnlyList<TEntity>> ListAllAsync();
    }
}
