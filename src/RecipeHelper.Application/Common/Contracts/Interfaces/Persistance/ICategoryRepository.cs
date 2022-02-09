using RecipeHelper.Domain.Entities;

namespace RecipeHelper.Application.Common.Contracts.Interfaces.Persistance
{
    public interface ICategoryRepository : IAsyncRepository<Category, Guid>
    {
        Task<Category?> GetByNameAsync(string name);
        Task<bool> IsCategoryNameUnique(string name, CancellationToken token);
    }
}
