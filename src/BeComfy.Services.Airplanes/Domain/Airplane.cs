using System;
using System.Collections.Generic;
using BeComfy.Common.Mongo;
using BeComfy.Common.Types.Enums;
using BeComfy.Common.Types.Exceptions;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace BeComfy.Services.Airplanes.Domain
{
    public class Airplane : IEntity
    {
        public Guid Id { get; private set; }
        public string AirplaneRegistrationNumber { get; private set; }
        public string Model { get; private set; }
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public IDictionary<SeatClass, int> AvailableSeats { get; private set; }
        public AirplaneStatus AirplaneStatus { get; private set; }
        public int FlightsCarriedOut { get; private set; }
        public DateTime? NextFlight { get; private set; }
        public DateTime? FlightEnd { get; private set; }
        public DateTime IntroductionToTheFleet { get; private set; }
        public DateTime UpdatedAt { get; private set; }
    
        public Airplane(Guid id, string airplaneRegistrationnumber, string model,
            IDictionary<SeatClass, int> availableSeats)
        {
            SetAirplaneId(id);
            SetAirplaneRegistrationNumber(airplaneRegistrationnumber);
            SetAirplaneModel(model);
            SetAvaiableSeats(availableSeats);
            AirplaneStatus = AirplaneStatus.Ready;
            FlightsCarriedOut = 0;
            NextFlight = DateTime.MinValue;
            FlightEnd = DateTime.MinValue;
            IntroductionToTheFleet = DateTime.UtcNow;
            UpdatedAt = DateTime.MinValue;
        }

        private void SetAirplaneId(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new BeComfyException("airplane_empty_id", "Airplane id cannot be empty");
            }

            Id = id;
        }

        private void SetAirplaneRegistrationNumber(string airplaneRegistrationnumber)
        {
            if (string.IsNullOrEmpty(airplaneRegistrationnumber) || airplaneRegistrationnumber.Length == 0)
            {
                throw new BeComfyException("airplane_invalid_registration_number", "Airplane registration number has incorrect form or it is empty");
            }

            AirplaneRegistrationNumber = airplaneRegistrationnumber;
        }

        private void SetAirplaneModel(string model)
        {
            if (string.IsNullOrWhiteSpace(model) || model.Length == 0)
            {
                throw new BeComfyException("airplane_invalid_model", "Airplane model cannot be null or empty");
            }

            Model = model;
        }

        private void SetAvaiableSeats(IDictionary<SeatClass, int> availableSeats)
        {
            if (availableSeats.Count <= 0)
            {
                throw new BeComfyDomainException("Count of seatsClass cannot be less or equal to 0");
            }

            if (GetSeatsCount(availableSeats) <= 0)
            {
                throw new BeComfyDomainException("Count of available seats cannot be less or equal to 0");
            }

            AvailableSeats = availableSeats;
            SetUpdateDate();
        }

        public void SetAirplaneStatus(AirplaneStatus airplaneStatus)
            => AirplaneStatus = airplaneStatus;

        public void SetNextFlight(DateTime? flightStart)
        {
            if (flightStart.HasValue)
            {
                NextFlight = flightStart;
            }
            else
            {
                throw new BeComfyDomainException("Flight cannot start before end of flight");
            }

            SetUpdateDate();
        }

        public void SetFlightEnd(DateTime? flightEnd)
        {
            if (flightEnd.HasValue)
            {
                FlightEnd = flightEnd;
            }
            else
            {
                throw new BeComfyDomainException("Flight cannot end before start of flight");
            }

            SetUpdateDate();
        }

        private int GetSeatsCount(IDictionary<SeatClass, int> seats)
        {
            int seatsCounter = 0;
            foreach (var seatClass in seats)
            {
                seatsCounter += seatClass.Value;
            }

            return seatsCounter;
        }

        public void IncreaseFlightsCarriedOut() 
        {
            FlightsCarriedOut++;
            SetUpdateDate();
        }

        public void SetUpdateDate()
            => UpdatedAt = DateTime.UtcNow;
    }
}