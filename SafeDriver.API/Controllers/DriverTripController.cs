using System;
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
using SafeDriver.Domain.ValueObjects;

namespace SafeDriver.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverTripController : ControllerBase
    {
        private readonly SafeDriverDbContext _dbContext;
        private readonly IMapper _mapper;

        public DriverTripController(SafeDriverDbContext dbContext, IMapper mapper) {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        [HttpPost("{driverUUID}/start")]
        public async Task<ActionResult> StartNewTrip(string driverUUID, StartNewTripInputModel startTripInput) {
            
            if(string.IsNullOrWhiteSpace(driverUUID) || startTripInput == null)
                return BadRequest();

            var newTrip = new Trip() {
                StartingCoordinates = new Coordinate() {
                    Latitude = startTripInput.StartLatitude,
                    Longitude = startTripInput.StartLongitude,
                },
                TripStartTimestamp = startTripInput.StartTimestamp
            };

            var driver = await _dbContext.Drivers.AsNoTracking()
                            .SingleOrDefaultAsync(d => d.DriverUUID == driverUUID);
            
            if(driver == null) return NotFound(); 

            newTrip.DriverId = driver.Id;
            _dbContext.Trips.Add(newTrip);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDriverTripById), new { driverUUID = driver.DriverUUID, tripId = newTrip.Id }, newTrip);
        }


        [HttpPut("{driverUUID}/finish/{tripId}")]
        public async Task<ActionResult> FinishTrip(string driverUUID, int tripId, FinishTripInputModel finishTripInput) {
            if(string.IsNullOrEmpty(driverUUID) || finishTripInput == null) return BadRequest();

            var driverFound = await _dbContext.Drivers.AsNoTracking()
                                    .SingleOrDefaultAsync(d => d.DriverUUID == driverUUID);
            if(driverFound == null) return NotFound();

            var tripFound = await _dbContext.Trips.AsNoTracking()
                            .SingleOrDefaultAsync(t => t.Id == tripId);
            if(tripFound == null) return NotFound();

            tripFound.FinalCoordinates = new Coordinate() {
                Latitude = finishTripInput.FinishLatitude,
                Longitude = finishTripInput.FinishLongitude
            };
            tripFound.TripDuration = (tripFound.TripStartTimestamp - finishTripInput.FinishTimestamp);

            _dbContext.Trips.Update(tripFound);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDriverTripById), new { driverUUID = driverUUID, tripId = tripId }, tripFound);
        }

        [HttpGet("{driverUUID}")]
        public async Task<ActionResult<List<GetTripInfoOutputModel>>> GetDriverTrips(string driverUUID) {
            if(string.IsNullOrEmpty(driverUUID)) return BadRequest();

            var driverFound = await _dbContext.Drivers.AsNoTracking()
                        .SingleOrDefaultAsync(d => d.DriverUUID == driverUUID);
            if(driverFound == null) return NotFound();


            var trips = await _dbContext.Trips.AsNoTracking()
                        .Where(t => t.DriverId == driverFound.Id)
                        .ToListAsync();

            var outputModelList = _mapper.Map<List<Trip>, List<GetTripInfoOutputModel>>(trips);

            return Ok(outputModelList);
        }

        [HttpGet("{driverUUID}/{tripId}")]
        public async Task<ActionResult<GetTripInfoOutputModel>> GetDriverTripById(string driverUUID, int tripId) {
            
            if(string.IsNullOrWhiteSpace(driverUUID) || tripId <= 0) return BadRequest();           
            
            var driverFound = await _dbContext.Drivers.AsNoTracking()
                                    .SingleOrDefaultAsync(d => d.DriverUUID == driverUUID);
            
            var tripFound = await _dbContext.Trips.AsNoTracking()
                                    .SingleOrDefaultAsync(t => t.DriverId == driverFound.Id && t.Id == tripId);
            
            if(driverFound == null || tripFound == null) return NotFound();

            var tripInfoOutput = _mapper.Map<Trip, GetTripInfoOutputModel>(tripFound);

            return Ok(tripInfoOutput);
        }
    }
}