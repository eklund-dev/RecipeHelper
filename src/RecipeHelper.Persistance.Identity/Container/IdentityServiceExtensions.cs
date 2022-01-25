using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeHelper.Application.Common.Contracts.Interfaces.Auth;
using RecipeHelper.Application.Common.Contracts.Interfaces.Identity;
using RecipeHelper.Application.Common.Contracts.Interfaces.Identity.Roles;
using RecipeHelper.Persistance.Identity.Context;
using RecipeHelper.Persistance.Identity.Models;
using RecipeHelper.Persistance.Identity.Services;

namespace RecipeHelper.Persistance.Identity.Container
{
    public static class IdentityServiceExtensions
    {
        private const string _allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+!";
        public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<RecipeHelperIdentityDbContext>(options =>
                    options.UseInMemoryDatabase("RecipeHelperIdentityDb"));
            }
            else
            {
                services.AddDbContext<RecipeHelperIdentityDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("RecipeIdentityConnectionString"),
                    b => b.MigrationsAssembly(typeof(RecipeHelperIdentityDbContext).Assembly.FullName)));
            }

            services.AddIdentity<ApplicationUser, ApplicationRole>()
               .AddEntityFrameworkStores<RecipeHelperIdentityDbContext>()
               .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = true;
                opt.User.RequireUniqueEmail = true;
                opt.User.AllowedUserNameCharacters = _allowedCharacters;
            });

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IIdentityUserService, IdentityUserService>();
            services.AddScoped<IIdentityRoleService, IdentityRoleService>();
        }
    }
}
