using RecipeHelper.Application.Common.Contracts.Interfaces;

namespace RecipeHelper.WebAPI.Installers
{
    public class CorsInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddCors(options =>
             {
                 options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
             });
        }
    }
}
