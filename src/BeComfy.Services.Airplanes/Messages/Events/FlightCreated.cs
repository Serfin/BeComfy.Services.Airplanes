using System;
using BeComfy.Common.CqrsFlow;
using BeComfy.Common.Messages;

namespace BeComfy.Services.Airplanes.Messages.Events
{
    [MessageNamespace("flights")]
    public class FlightCreated : IEvent
    {
        public Guid Id { get; set; }
    }
}