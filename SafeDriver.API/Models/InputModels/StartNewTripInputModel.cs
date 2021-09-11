using System;

namespace SafeDriver.API.Models.InputModels
{
    public class StartNewTripInputModel {
        
        public double StartLatitude { get; set; }
        public double StartLongitude { get; set; }
        public DateTime StartTimestamp { get; set; }
    }    
}