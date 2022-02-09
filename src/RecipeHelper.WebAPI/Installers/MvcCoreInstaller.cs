﻿using FluentValidation.AspNetCore;
using Newtonsoft.Json.Serialization;
using RecipeHelper.Application.Common.Contracts.Interfaces;
using RecipeHelper.Application.Common.Validators;
using RecipeHelper.Application.Features.Ingredients.Validators;

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
                fvc.RegisterValidatorsFromAssemblyContaining<CreateIngredientValidator>();
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", builder => builder.RequireClaim("admin.view", "true"));
            });
        }
    
    }
}
