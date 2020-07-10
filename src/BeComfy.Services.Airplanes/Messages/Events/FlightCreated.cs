using System;
using BeComfy.Common.CqrsFlow;
using BeComfy.Common.Messages;
using Newtonsoft.Json;

namespace BeComfy.Services.Airplanes.Messages.Events
{
    [MessageNamespace("flights")]
    public class FlightCreated : IEvent
    {
        public Guid FlightId { get; set; }
        public Guid AirplaneId { get; set; }
        public DateTime FlightStart { get; set; }
        public DateTime FlightEnd { get; set; }
        
        [JsonConstructor]
        public FlightCreated(Guid flightId, Guid airplaneId, 
            DateTime flightStart, DateTime flightEnd)
        {
            FlightId = flightId;
            AirplaneId = airplaneId;
            FlightStart = flightStart;
            FlightEnd = flightEnd;
        }
    }
}