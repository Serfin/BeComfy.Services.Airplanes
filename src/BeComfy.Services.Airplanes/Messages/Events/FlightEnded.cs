using System;
using BeComfy.Common.CqrsFlow;
using BeComfy.Common.Messages;
using Newtonsoft.Json;

namespace BeComfy.Services.Airplanes.Messages.Events
{
    [MessageNamespace("flights")]
    public class FlightEnded : IEvent
    {
        public Guid PlaneId { get; }
        
        [JsonConstructor]
        public FlightEnded(Guid planeId)
        {
            PlaneId = planeId;
        }
    }
}