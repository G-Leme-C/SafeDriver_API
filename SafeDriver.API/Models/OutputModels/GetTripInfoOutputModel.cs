using System;
using SafeDriver.Domain.ValueObjects;

namespace SafeDriver.API.Models.OutputModels
{
    public class GetTripInfoOutputModel
    {
        public int Id { get; set; }
        public Coordinate StartingCoordinates { get; set; }
        public Coordinate FinalCoordinates { get; set; }
        public string DriverUUID { get; set; }
        public DateTime TripStartTimestamp { get; set; }
        public TimeSpan TripDuration { get; set; }
    }
}