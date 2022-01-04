using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeHelper.Application.Common.Contracts.Persistance;
using RecipeHelper.Persistance.Data.Context;
using RecipeHelper.Persistance.Data.Repositories.Base;

namespace RecipeHelper.Persistance.Data.Container
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RecipeHelperDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("RecipeConnectionstring")));
            
            services.AddScoped(typeof(IAsyncCommandRepository<>), typeof(BaseCommandRepository<>));
            services.AddScoped(typeof(IAsyncReadRepository<,>), typeof(BaseReadRepository<,>));

            return services;
        }
    }
}
