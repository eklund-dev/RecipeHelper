using Microsoft.AspNetCore.Mvc;

namespace RecipeHelper.WebAPI.Controllers.v1
{
    [Route("api")]
    [ApiController]
    public class ApiDataController : ControllerBase
    {
        public ApiDataController()
        {

        }

        [HttpGet("/version")]
        public async Task<IActionResult> GetApiVersion()
        {
            return Ok();
        }

        [HttpGet("/ping")]
        public async Task<IActionResult> Ping()
        {
            return Ok("Ping Pong Ping Pong");
        }
    }
}
