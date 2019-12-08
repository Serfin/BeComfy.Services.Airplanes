using System.Threading.Tasks;
using BeComfy.Common.CqrsFlow.Handlers;
using BeComfy.Common.RabbitMq;
using BeComfy.Common.Types.Exceptions;
using BeComfy.Services.Airplanes.Messages.Events;
using BeComfy.Services.Airplanes.Repositories;

namespace BeComfy.Services.Airplanes.EventHandlers
{
    public class FlightCreatedHandler : IEventHandler<FlightCreated>
    {
        private readonly IAirplanesRepository _airplanesRepository;
        private readonly IBusPublisher _busPublisher;

        public FlightCreatedHandler(IAirplanesRepository airplanesRepository, IBusPublisher busPublisher)
        {
            _airplanesRepository = airplanesRepository;
            _busPublisher = busPublisher;
        }
        public async Task HandleAsync(FlightCreated @event, ICorrelationContext context)
        {
            var airplane = await _airplanesRepository.GetAsync(@event.AirplaneId);

            if (airplane is null)
            {
                throw new BeComfyException($"Airplane with id '{@event.AirplaneId}' does not exist");
            }

            airplane.SetNextFlight(@event.FlightStart);
            airplane.SetFlightEnd(@event.FlightEnd);
            airplane.SetAirplaneStatus(Domain.AirplaneStatus.Reserved);

            await _airplanesRepository.UpdateAsync(airplane);
        }
    }
}