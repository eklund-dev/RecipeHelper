using System.Security.Claims;

namespace RecipeHelper.Application.Common.Contracts
{
    public interface IHttpContextUserService
    {
        ClaimsPrincipal? GetUser();
    }
}
