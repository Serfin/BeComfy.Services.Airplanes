using System;
using System.Collections.Generic;
using BeComfy.Common.Types.Enums;

namespace BeComfy.Services.Airplanes.Dto
{
    public class AirplaneDto
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string AirplaneRegistrationNumber { get; set; }
        public AirplaneStatus AirplaneStatus { get; set; }
        public IDictionary<SeatClass, int> AvailableSeats { get; set; }
        public IDictionary<EmployeePosition, int> RequiredSeats { get; set; }
        public DateTime? NextFlight { get; set; }
        public DateTime? FlightEnd { get; set; }
    }
}