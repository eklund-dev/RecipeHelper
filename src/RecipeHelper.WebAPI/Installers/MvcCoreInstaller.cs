using FluentValidation.AspNetCore;
using Newtonsoft.Json.Serialization;
using RecipeHelper.Application.Common.Contracts;

namespace RecipeHelper.WebAPI.Installers
{
    public class MvcCoreInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddHealthChecks();

            services.AddMvcCore(options =>
            {
                options.EnableEndpointRouting = false;
                //options.Filters.Add(new ControllerValidationFilter());
            })
            .AddFluentValidation(mvcConfig =>
             mvcConfig.RegisterValidatorsFromAssemblyContaining<Program>());

            services.AddAuthorization();
        }
    
    }
}
