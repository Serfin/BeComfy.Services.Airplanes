using Microsoft.AspNetCore.Mvc;

namespace BeComfy.Services.Airplanes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
            => Ok("BeComfy Airplanes Microservice");
    }
}
