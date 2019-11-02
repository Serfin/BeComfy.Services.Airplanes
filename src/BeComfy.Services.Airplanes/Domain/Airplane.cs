using System;
using System.Collections.Generic;
using BeComfy.Common.Types.Enums;

namespace BeComfy.Services.Airplanes.Domain
{
    public class Airplane
    {
        public Guid Id { get; private set; }
        public IDictionary<SeatClass, int> AvailableSeats { get; private set; }
        public int FlightsCarriedOut { get; private set; }
        public DateTime? NextFlight { get; private set; }
        public DateTime IntroductionToTheFleet { get; private set; }
    }
}