using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeDriver.API.Models.InputModels;
using SafeDriver.Domain.Data;

namespace SafeDriver.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly SafeDriverDbContext _safeDriverDbContext;

        public LoginController(SafeDriverDbContext safeDriverDbContext)
        {
            this._safeDriverDbContext = safeDriverDbContext;
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginInputModel loginInputModel)
        {
            var driverFound = await _safeDriverDbContext.Drivers
                .Where(driver => driver.EmailAddress.Trim().Equals(loginInputModel.Email.Trim()) && driver.Password.Equals(loginInputModel.Senha))
                .FirstOrDefaultAsync();

            if(driverFound != null) {
                return Ok(new {
                    driverUUID = driverFound.DriverUUID
                });
            }

            return NotFound();
        }


    }
}