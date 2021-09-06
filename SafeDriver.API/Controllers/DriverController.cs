using Microsoft.AspNetCore.Mvc;

namespace SafeDriver.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetDriverById(int driverId) {
            return Ok("Teste");
        }
        
    }
    
}