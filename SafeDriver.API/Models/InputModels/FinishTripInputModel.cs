using System;

namespace SafeDriver.API.Models.InputModels
{
    public class FinishTripInputModel {
        public double FinishLatitude { get; set; }
        public double FinishLongitude { get; set; }
        public DateTime FinishTimestamp { get; set; }
    }    
}