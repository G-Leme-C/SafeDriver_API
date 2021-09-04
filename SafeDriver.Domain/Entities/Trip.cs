using System;
using SafeDriver.Domain.ValueObjects;

namespace SafeDriver.Domain.Entities
{
    public class Trip {
        public int Id { get; set; }
        public Coordinate StartingCoordinates { get; set; }
        public Coordinate FinalCoordinates { get; set; }

        public int DriverId { get; set; }
        public Driver Driver { get; set; }

        public TimeSpan TripDuration { get; set; }
    }
}