using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RecipeHelper.WebAPI.Controllers.v1.Base
{
    [ApiController]
    [Route("")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    }
}
