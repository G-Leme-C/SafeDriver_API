using System;
using SafeDriver.Domain.Enums;
using SafeDriver.Domain.ValueObjects;

namespace SafeDriver.API.Models.InputModels
{
    public class CreateEventInputModel
    {
        public DateTime EventDateTime { get; set; }
        public Coordinate Coordinates { get; set; }
        public int GeneratedScore { get; set; }
        public EEventType EventType { get; set; }
        public decimal PrecisionLevel { get; set; }
    }
}