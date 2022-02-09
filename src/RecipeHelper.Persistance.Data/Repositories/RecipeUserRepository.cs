using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Domain.Entities;
using RecipeHelper.Persistance.Data.Context;
using RecipeHelper.Persistance.Data.Repositories.Base;

namespace RecipeHelper.Persistance.Data.Repositories
{
    public class RecipeUserRepository : BaseRepository<RecipeUser, Guid>, IRecipeUserRepository
    {
        public RecipeUserRepository(RecipeHelperDbContext dbContext) : base(dbContext)
        {
        }
    }
}
