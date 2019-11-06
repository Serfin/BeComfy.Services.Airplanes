using System;
using BeComfy.Common.CqrsFlow;
using Newtonsoft.Json;

namespace BeComfy.Services.Airplanes.Messages.Events
{
    public class AirplaneCreated : IEvent
    {
        public Guid Id { get; set; }
        public string Model { get; set; }

        [JsonConstructor]
        public AirplaneCreated(Guid id, string model)
        {
            Id = id;
            Model = model;
        }
    }
}