using Microsoft.EntityFrameworkCore;
using RecipeHelper.Application.Common.Contracts.Interfaces.Persistance;
using RecipeHelper.Domain.Entities;
using RecipeHelper.Persistance.Data.Context;
using RecipeHelper.Persistance.Data.Repositories.Base;

namespace RecipeHelper.Persistance.Data.Repositories
{
    public class FoodTypeRepository : BaseRepository<FoodType, Guid>, IFoodTypeRepository
    {
        public FoodTypeRepository(RecipeHelperDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> FoodTypeExistsAsync(Guid id)
        {
            var entity = await _dbContext.FoodTypes.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            return entity == null ? false : true;
        }
    }
}
