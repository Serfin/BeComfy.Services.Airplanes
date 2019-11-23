using System;
using BeComfy.Common.CqrsFlow;
using Newtonsoft.Json;

namespace BeComfy.Services.Airplanes.Messages.Commands
{
    public class ReserveAirplane : ICommand
    {
        public Guid Id { get; }
        public DateTime FlightStart { get; }
        public DateTime FlightEnd { get; }

        [JsonConstructor]
        public ReserveAirplane(Guid id, DateTime flightStart, DateTime flightEnd)
        {
            Id = id;
            FlightStart = flightStart;
            FlightEnd = flightEnd;
        }
    }
}