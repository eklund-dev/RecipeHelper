using System.Security.Claims;

namespace RecipeHelper.Application.Common.Contracts.Interfaces
{
    public interface IHttpContextUserService
    {
        ClaimsPrincipal? GetUser();

        string GetClaimsUserName();

        string GetClaimsUserId();
    }
}
