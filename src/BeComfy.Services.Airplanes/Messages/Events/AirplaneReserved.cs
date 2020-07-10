using System;
using System.Collections.Generic;
using BeComfy.Common.CqrsFlow;
using BeComfy.Common.Types.Enums;
using Newtonsoft.Json;

namespace BeComfy.Services.Airplanes.Messages.Events
{
    public class AirplaneReserved : IEvent
    {
        public Guid AirplaneId { get; }
        public Guid FlightId { get; }
        public IDictionary<EmployeePosition, int> RequiredCrew { get; }

        [JsonConstructor]
        public AirplaneReserved(Guid airplaneId, Guid flightId, 
            IDictionary<EmployeePosition, int> requiredCrew)
        {
            AirplaneId = airplaneId;
            FlightId = flightId;
            RequiredCrew = requiredCrew;
        }
    }
}