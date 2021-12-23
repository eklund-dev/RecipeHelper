using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeHelper.Persistance.Data.Context;

namespace RecipeHelper.Persistance.Data.Container
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RecipeHelperDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CleanSavingsConnectionString")));

            //services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            //services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<ISavingsRepository, SavingsEntityRepository>();
            //services.AddScoped<IUserProfileRepository, UserProfileRepository>();

            return services;
        }
    }
}
