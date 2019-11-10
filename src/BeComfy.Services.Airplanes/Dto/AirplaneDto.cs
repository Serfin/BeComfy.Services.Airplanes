using System;
using BeComfy.Services.Airplanes.Domain;

namespace BeComfy.Services.Airplanes.Dto
{
    public class AirplaneDto
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public AirplaneStatus AirplaneStatus { get; set; }
        public DateTime? NextFlight { get; set; }
        public DateTime? FlightEnd { get; set; }
    }
}