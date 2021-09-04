using System;
using SafeDriver.Domain.Enums;
using SafeDriver.Domain.ValueObjects;

namespace SafeDriver.Domain.Entities {
    public class Event {
        public int Id { get; set; }
        public DateTime EventDateTime { get; set; }
        public Coordinate Coordinates { get; set; }
        public int GeneratedScore { get; set; }
        public EEventType EventType { get; set; }
        public decimal PrecisionLevel { get; set; }
    }
}