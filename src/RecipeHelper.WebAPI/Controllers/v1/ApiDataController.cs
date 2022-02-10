using Microsoft.AspNetCore.Mvc;

namespace RecipeHelper.WebAPI.Controllers.v1
{
    [Route("api/data")]
    [ApiController]
    public class ApiDataController : ControllerBase
    {
        public ApiDataController()
        {

        }

        //[HttpGet("/version")]
        //public async Task<IActionResult> GetApiVersion()
        //{
        //    return Ok();
        //}

        [HttpGet("/ping")]
        public IActionResult Ping()
        {
            return Ok("Pong");
        }
    }
}
