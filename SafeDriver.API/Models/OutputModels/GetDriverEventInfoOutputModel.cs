using System;
using SafeDriver.Domain.Enums;
using SafeDriver.Domain.ValueObjects;

namespace SafeDriver.API.Models.OutputModels
{
    public class GetDriverEventInfoOutputModel
    {
        public int Id { get; set; }
        public DateTime EventDateTime { get; set; }
        public Coordinate Coordinates { get; set; }
        public int GeneratedScore { get; set; }
        public EEventType EventType { get; set; }
        public string EventTypeDescription 
        { 
            get => this.EventType.ToString();
        }
        public decimal PrecisionLevel { get; set; }
    }
}