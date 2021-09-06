using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SafeDriver.API.Models.InputModels;
using SafeDriver.Domain.Data;
using SafeDriver.Domain.Entities;

namespace SafeDriver.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SafeDriverDbContext _dbContext;

        public DriverController(IMapper mapper, SafeDriverDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;

        }
        [HttpGet]
        public IActionResult GetDriverByUUID(string driverUUID)
        {
            return Ok("Teste");
        }

        [HttpPost]
        public IActionResult CreateNewDriver(CreateDriverInputModel driverInputModel)
        {
            var driver = _mapper.Map<CreateDriverInputModel, Driver>(driverInputModel);

            _dbContext.Drivers.Add(driver);
            _dbContext.SaveChanges();

            return Created(nameof(GetDriverByUUID), driver.DriverUUID);
        }
    }

}