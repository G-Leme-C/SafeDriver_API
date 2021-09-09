using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult<List<GetDriverEventInfoOutputModel>>> GetAllEventsFromDriver(string driverUUID)
        {
            if(string.IsNullOrWhiteSpace(driverUUID))
                return BadRequest();

            var driver = await _dbContext.Drivers.AsNoTracking()
                        .SingleOrDefaultAsync(d => d.DriverUUID == driverUUID);

            if(driver == null) return NotFound();

            var events = await _dbContext.Events.AsNoTracking()
                        .Where(e => e.DriverId == driver.Id)
                        .ToListAsync();

            return _mapper.Map<List<Event>, List<GetDriverEventInfoOutputModel>>(events);
        }

        [HttpGet("{driverUUID}/{eventId}")]
        public async Task<ActionResult<GetDriverEventInfoOutputModel>> GetEventById(string driverUUID, int eventId) {
            if(string.IsNullOrWhiteSpace(driverUUID) || eventId <= 0)
                return BadRequest();

            var driver = await _dbContext.Drivers
                            .AsNoTracking()
                            .SingleOrDefaultAsync(d => d.DriverUUID == driverUUID);

            if(driver == null) return NotFound();
            
            var eventById = await _dbContext.Events
                            .AsNoTracking()
                            .SingleOrDefaultAsync(e => e.Id == eventId && e.DriverId == driver.Id);

            if(eventById == null) return NotFound();

            return _mapper.Map<Event, GetDriverEventInfoOutputModel>(eventById);
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

            _dbContext.Events.Add(newEvent);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEventById), new { driverUUID = driver.DriverUUID, eventId = newEvent.Id }, newEvent);
        }
    }
}