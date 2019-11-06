using System;
using BeComfy.Common.CqrsFlow;
using Newtonsoft.Json;

namespace BeComfy.Services.Airplanes.Messages.Events
{
    public class AirplaneReserved : IEvent
    {
        public Guid Id { get; set; }
        public DateTime FlightStart { get; set; }
        public DateTime FlightEnd { get; set; }
        
        [JsonConstructor]
        public AirplaneReserved(Guid id, DateTime flightStart, DateTime flightEnd)
        {
            Id = id;
            FlightStart = flightStart;
            FlightEnd = flightEnd;
        }
    }
}