using Microsoft.AspNetCore.Http;
using RecipeHelper.Application.Common.Contracts;
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

    }
}
