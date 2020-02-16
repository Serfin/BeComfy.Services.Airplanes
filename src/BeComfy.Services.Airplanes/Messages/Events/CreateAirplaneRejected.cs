using System;
using BeComfy.Common.CqrsFlow;
using Newtonsoft.Json;

namespace BeComfy.Services.Airplanes.Messages.Events
{
    public class CreateAirplaneRejected : IRejectedEvent
    {
        public Guid Id { get; }
        public string AirplaneRegistrationNumber { get; }
        public string Model { get; }
        public string Code { get; }
        public string Reason { get; }

        [JsonConstructor]
        public CreateAirplaneRejected(Guid id, string airplaneRegistrationNumber, string model, string code, string reason)
        {
            Id = id;
            AirplaneRegistrationNumber = airplaneRegistrationNumber;
            Model = model;
            Code = code;
            Reason = reason;
        }
    }
}