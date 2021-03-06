using System;
using System.Threading.Tasks;
using BeComfy.Common.CqrsFlow.Handlers;
using BeComfy.Common.RabbitMq;
using BeComfy.Common.Types.Enums;
using BeComfy.Common.Types.Exceptions;
using BeComfy.Services.Airplanes.Messages.Events;
using BeComfy.Services.Airplanes.Repositories;

namespace BeComfy.Services.Airplanes.EventHandlers
{
    public class FlightEndedHandler : IEventHandler<FlightEnded>
    {
        private readonly IAirplanesRepository _airplanesRepository;

        public FlightEndedHandler(IAirplanesRepository airplanesRepository)
        {
            _airplanesRepository = airplanesRepository;
        }
        public async Task HandleAsync(FlightEnded @event, ICorrelationContext context)
        {
            var airplane = await _airplanesRepository.GetAsync(@event.PlaneId);

            if (airplane is null)
            {
                throw new BeComfyException("cannot_end_flight", $"Cannot end flight, airplane with id: '{@event.PlaneId}' does not exist");
            }

            airplane.IncreaseFlightsCarriedOut();
            airplane.SetNextFlight(DateTime.MinValue);
            airplane.SetFlightEnd(DateTime.MinValue);
            airplane.SetAirplaneStatus(AirplaneStatus.Ready);

            await _airplanesRepository.UpdateAsync(airplane);
        }
    }
}