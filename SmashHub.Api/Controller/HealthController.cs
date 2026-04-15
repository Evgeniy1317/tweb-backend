using Microsoft.AspNetCore.Mvc;


namespace SmashHub.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("API is working");
        }
    }
}
