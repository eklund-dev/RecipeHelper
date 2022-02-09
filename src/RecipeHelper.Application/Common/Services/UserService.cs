using Microsoft.AspNetCore.Http;
using RecipeHelper.Application.Common.Contracts.Interfaces;
using System.Security.Claims;

namespace RecipeHelper.Application.Common.Services
{
    public class UserService : IHttpContextUserService
    {
        private readonly IHttpContextAccessor _accessor;

        public UserService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public ClaimsPrincipal? GetUser() => _accessor?.HttpContext?.User;

        public string GetClaimsUserName()
        {
            return GetUser()?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
        }

        public string GetClaimsUserId()
        {
            return GetUser()?.FindFirst("uid")?.Value ?? "";
        }
    }
}
