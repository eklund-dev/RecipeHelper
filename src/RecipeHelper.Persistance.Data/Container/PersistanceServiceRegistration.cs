﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeHelper.Application.Common.Contracts.Persistance;
using RecipeHelper.Persistance.Data.Context;
using RecipeHelper.Persistance.Data.Repositories;
using RecipeHelper.Persistance.Data.Repositories.Base;

namespace RecipeHelper.Persistance.Data.Container
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<RecipeHelperDbContext>(options =>
                    options.UseInMemoryDatabase("RecipeHelperDb"));
            }
            else
            {
                services.AddDbContext<RecipeHelperDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("RecipeConnectionstring"),
                        b => b.MigrationsAssembly(typeof(RecipeHelperDbContext).Assembly.FullName)));
            }
            
            services.AddTransient(typeof(IAsyncRepository<,>), typeof(BaseRepository<,>));
            services.AddTransient<IRecipeRepository, RecipeRepository>();
            services.AddTransient<IIngredientRepository, IngredientRepository>();

            return services;
        }
    }
}
