using System.Collections.Generic;
using System.Linq;
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
    public class DriverEventController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SafeDriverDbContext _dbContext;

        public DriverEventController(IMapper mapper, SafeDriverDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        [HttpGet("{driverUUID}")]
        public async Task<ActionResult<List<Event>>> GetAllEventsFromDriver(string driverUUID)
        {
            if(string.IsNullOrWhiteSpace(driverUUID))
                return BadRequest();

            var driver = await _dbContext.Drivers.AsNoTracking()
                        .SingleOrDefaultAsync(d => d.DriverUUID == driverUUID);

            if(driver == null) return NotFound();

            var events = await _dbContext.Events.AsNoTracking()
                        .Where(e => e.DriverId == driver.Id)
                        .ToListAsync();

            return events;
        }

        [HttpPost("{driverUUID}")]
        public async Task<ActionResult> CreateEvent(string driverUUID, CreateEventInputModel eventInputModel) {
            if(string.IsNullOrWhiteSpace(driverUUID) || eventInputModel == null) {
                return BadRequest();
            }

            var newEvent = _mapper.Map<CreateEventInputModel, Event>(eventInputModel); 

            var driver = await _dbContext.Drivers.AsNoTracking()
                            .SingleOrDefaultAsync(d => d.DriverUUID == driverUUID);
            
            if(driver == null) return NotFound();

            newEvent.DriverId = driver.Id;

            return CreatedAtAction(nameof(GetAllEventsFromDriver), new { driverUUID = driver.DriverUUID });
        }
    }
}