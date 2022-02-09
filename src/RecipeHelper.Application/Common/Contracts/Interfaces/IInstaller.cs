using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RecipeHelper.Application.Common.Contracts.Interfaces
{
    public interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration Configuration);
    }
}
