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

        [HttpGet("{driverUUID}/statistics")]
        public async Task<ActionResult> GetDriverStatsByUUID(string driverUUID) {
            return Ok(new {
                DriverScore = 537,
                TripsCompleted = 15,
                Achievements = 10,
                BonusesAvailable = 4,
                RegisteredAlerts = 98
            });
        }

        [HttpGet("{driverUUID}/bonuses")]
        public async Task<ActionResult> GetDriverBonusesByUUID(string driverUUID) {
            return Ok(new[] {
                new 
                {
                    BonusId = 1,
                    Vendor = "Sem parar",
                    PromotionText = "35% de desconto"
                },
                new 
                {
                    BonusId = 2,
                    Vendor = "Porto Seguro",
                    PromotionText = "Até 20% de desconto"
                },
                new 
                {
                    BonusId = 3,
                    Vendor = "Bradesco Seguros Auto",
                    PromotionText = "Até 25% de desconto"
                }
            }
            );
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