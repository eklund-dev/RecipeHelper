using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeHelper.Application.Common.Contracts;
using RecipeHelper.Application.Common.Services;
using RecipeHelper.Persistance.Data.Container;
using RecipeHelper.Persistance.Identity.Containers;

namespace RecipeHelper.Infrastructure.IoC.Container
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IHttpContextUserService, UserService>();

            services.AddIdentityServices(configuration);

            services.AddJwtServices(configuration);

            services.AddPersistanceServices(configuration);
        }
    }
}
