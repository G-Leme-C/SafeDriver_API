using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeDriver.API.Models.InputModels;
using SafeDriver.API.Models.OutputModels;
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
        public async Task<ActionResult<GetDriverInfoOutputModel>> GetDriverByUUID(string driverUUID)
        {
            if(string.IsNullOrWhiteSpace(driverUUID))
                return BadRequest();
            
            var driver = await _dbContext.Drivers
                        .AsNoTracking()
                        .SingleOrDefaultAsync(d => d.DriverUUID == driverUUID);

            if(driver == null)
                return NotFound();

            return _mapper.Map<Driver, GetDriverInfoOutputModel>(driver);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewDriver(CreateDriverInputModel driverInputModel)
        {
            var driver = _mapper.Map<CreateDriverInputModel, Driver>(driverInputModel);

            Guid driverUUID = Guid.NewGuid();
            driver.DriverUUID = driverUUID.ToString();

            _dbContext.Drivers.Add(driver);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDriverByUUID), new { driverUUID = driver.DriverUUID }, driver);
        }
    }

}