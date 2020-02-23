using System.Threading.Tasks;
using BeComfy.Common.CqrsFlow.Handlers;
using BeComfy.Services.Airplanes.Dto;
using BeComfy.Services.Airplanes.Queries;
using BeComfy.Services.Airplanes.Repositories;

namespace BeComfy.Services.Airplanes.QueryHandlers
{
    public class GetAirplaneHandler : IQueryHandler<GetAirplane, AirplaneDto>
    {
        private readonly IAirplanesRepository _repository;

        public GetAirplaneHandler(IAirplanesRepository repository)
        {
            _repository = repository;
        }

        public async Task<AirplaneDto> HandleAsync(GetAirplane query)
        {
            var airplane = await _repository.GetAsync(query.Id);

            if (airplane != null)
            {
                return new AirplaneDto()
                {
                    Id = airplane.Id,
                    Model = airplane.Model,
                    AirplaneRegistrationNumber = airplane.AirplaneRegistrationNumber,
                    AirplaneStatus = airplane.AirplaneStatus,
                    AvailableSeats = airplane.AvailableSeats,
                    NextFlight = airplane.NextFlight,
                    FlightEnd = airplane.FlightEnd
                };
            }

            return null;
        }
    }
}