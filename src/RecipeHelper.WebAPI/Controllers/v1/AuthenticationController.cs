using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecipeHelper.WebAPI.Routes.v1;

namespace RecipeHelper.WebAPI.Controllers.v1
{
    [ApiController]
    [Route(ApiRoutes.ApiVersion1)]
    public class AuthenticationController : ControllerBase
    {
        public AuthenticationController()
        {

        }
    }
}
