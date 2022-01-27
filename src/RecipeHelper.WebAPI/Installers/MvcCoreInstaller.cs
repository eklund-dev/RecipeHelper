using FluentValidation.AspNetCore;
using Newtonsoft.Json.Serialization;
using RecipeHelper.Application.Common.Contracts;
using RecipeHelper.Application.Common.Validators;
using RecipeHelper.Application.Common.Validators.Auth;

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
                options.Filters.Add(new GlobalValidationFilter());
            })
            .AddFluentValidation(fvc =>
            {
                fvc.RegisterValidatorsFromAssemblyContaining<AuthRequestValidator>();
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", builder => builder.RequireClaim("admin.view", "true"));
            });
        }
    
    }
}
