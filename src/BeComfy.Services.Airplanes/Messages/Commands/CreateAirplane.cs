using System;
using System.Collections.Generic;
using BeComfy.Common.CqrsFlow;
using BeComfy.Common.Types.Enums;
//using BeComfy.MessageBroker.RabbitMQ.Messages;
using Newtonsoft.Json;

namespace BeComfy.Services.Airplanes.Messages.Commands
{
    public class CreateAirplane : ICommand //, IMessage
    {
        public Guid AirplaneId { get; }
        public string AirplaneRegistrationNumber { get; }
        public string AirplaneModel { get; }
        public IDictionary<SeatClass, int> AvailableSeats { get; }  
        public IDictionary<EmployeePosition, int> RequiredCrew { get; }

        [JsonConstructor]
        public CreateAirplane(Guid airplaneId, string airplaneRegistrationNumber, string airplaneModel, 
            IDictionary<SeatClass, int> availableSeats, IDictionary<EmployeePosition, int> requiredCrew)
        {   
            AirplaneId = airplaneId;
            AirplaneRegistrationNumber = airplaneRegistrationNumber;
            AirplaneModel = airplaneModel;
            AvailableSeats = availableSeats;
            RequiredCrew = requiredCrew;
        }
    }
}