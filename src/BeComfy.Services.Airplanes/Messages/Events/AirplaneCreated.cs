using System;
using BeComfy.Common.CqrsFlow;
using Newtonsoft.Json;

namespace BeComfy.Services.Airplanes.Messages.Events
{
    public class AirplaneCreated : IEvent
    {
        public Guid Id { get; set; }
        public string AirplaneRegistrationNumber { get; set; }
        public string Model { get; set; }

        [JsonConstructor]
        public AirplaneCreated(Guid id, string airplaneRegistrationNumber, string model)
        {
            Id = id;
            AirplaneRegistrationNumber = airplaneRegistrationNumber;
            Model = model;
        }
    }
}