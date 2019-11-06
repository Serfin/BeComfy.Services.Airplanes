using System;
using BeComfy.Common.CqrsFlow;
using Newtonsoft.Json;

namespace BeComfy.Services.Airplanes.Messages.Commands
{
    public class ReserveAirplane : ICommand
    {
        public Guid Id { get; set; }
        public DateTime FlightStart { get; set; }
        public DateTime FlightEnd { get; set; }

        [JsonConstructor]
        public ReserveAirplane(Guid id, DateTime flightStart, DateTime flightEnd)
        {
            Id = id;
            FlightStart = flightStart;
            FlightEnd = flightEnd;
        }
    }
}