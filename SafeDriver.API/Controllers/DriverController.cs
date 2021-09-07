using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpGet("{driverUUID}")]
        public async Task<ActionResult<Driver>> GetDriverByUUID(string driverUUID)
        {
            if(string.IsNullOrWhiteSpace(driverUUID))
                return BadRequest();
            
            var driver = await _dbContext.Drivers
                        .AsNoTracking()
                        .SingleOrDefaultAsync(d => d.DriverUUID == driverUUID);

            if(driver == null)
                return NotFound();

            return driver;
        }

        [HttpPost]
        public IActionResult CreateNewDriver(CreateDriverInputModel driverInputModel)
        {
            var driver = _mapper.Map<CreateDriverInputModel, Driver>(driverInputModel);

            Guid driverUUID = Guid.NewGuid();
            driver.DriverUUID = driverUUID.ToString();

            _dbContext.Drivers.Add(driver);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetDriverByUUID), new { driverUUID = driver.DriverUUID }, driver);
        }
    }

}