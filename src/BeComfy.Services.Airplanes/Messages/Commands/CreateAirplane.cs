using System;
using System.Collections.Generic;
using BeComfy.Common.CqrsFlow;
using BeComfy.Common.Types.Enums;
using Newtonsoft.Json;

namespace BeComfy.Services.Airplanes.Messages.Commands
{
    public class CreateAirplane : ICommand
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public IDictionary<SeatClass, int> AvailableSeats { get; set; }

        [JsonConstructor]
        public CreateAirplane(Guid id, string model, IDictionary<SeatClass, int> availableSeats)
        {   
            Id = id;
            Model = model;
            AvailableSeats = availableSeats;
        }
    }
}