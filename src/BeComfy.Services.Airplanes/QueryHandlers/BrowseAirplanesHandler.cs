using System.Collections.Generic;
using System.Threading.Tasks;
using BeComfy.Common.CqrsFlow.Handlers;
using BeComfy.Services.Airplanes.Dto;
using BeComfy.Services.Airplanes.Queries;
using BeComfy.Services.Airplanes.Repositories;

namespace BeComfy.Services.Airplanes.QueryHandlers
{
    public class BrowseAirplanesHandler : IQueryHandler<BrowseAirplanes, IEnumerable<AirplaneDto>>
    {
        private readonly IAirplanesRepository _airplanesRepository;

        public BrowseAirplanesHandler(IAirplanesRepository airplanesRepository)
        {
            _airplanesRepository = airplanesRepository;
        }

        public async Task<IEnumerable<AirplaneDto>> HandleAsync(BrowseAirplanes query)
        {
            var airplanes = await _airplanesRepository.BrowseAsync(query.PageSize, query.Page, query.Status);
            
            var temp = new List<AirplaneDto>();
            if (airplanes != null) 
            {
                foreach (var airplane in airplanes)
                {
                    temp.Add(new AirplaneDto() 
                        {  
                            Id = airplane.Id,
                            Model = airplane.Model,
                            AirplaneStatus = airplane.AirplaneStatus,
                            NextFlight = airplane.NextFlight,
                            FlightEnd = airplane.FlightEnd
                        });
                }
            }

            return temp;
        }
    }
}