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
    
        public Airplane(Guid id, IDictionary<SeatClass, int> availableSeats,
            DateTime? nextFlight, DateTime introductionToTheFleet)
        {
            Id = id;
            AvailableSeats = availableSeats;
            FlightsCarriedOut = 0;
            NextFlight = nextFlight;
            IntroductionToTheFleet = introductionToTheFleet;
        }

        public void IncreaseFlighitsCarriedOut() 
            => FlightsCarriedOut++;
    }
}