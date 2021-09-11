using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SafeDriver.API.Controllers
{
    public class DriverTripController : ControllerBase
    {
        public DriverTripController() {
            
        }

        [HttpPost("{driverUUID}/start")]
        public async Task<ActionResult> StartNewTrip(string driverUUID) {
            return Ok();
        }


        [HttpPost("{driverUUID}/finish")]
        public async Task<ActionResult> FinishTrip(string driverUUID) {
            return Ok();
        }

        [HttpGet("{driverUUID}")]
        public async Task<ActionResult> GetDriverTrips(string driverUUID) {
            return Ok();
        }

        [HttpGet("{driverUUID}/{tripId}")]
        public async Task<ActionResult> GetDriverTripById(string driverUUID, int tripId) {
            return Ok();
        }
    }
}