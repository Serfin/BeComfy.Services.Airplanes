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
            var airplane = await _airplanesRepository.GetAirplaneAsync(@event.Id);

            if (airplane is null)
            {
                throw new BeComfyException($"Airplane with id '{@event.Id}' does not exist");
            }

            airplane.IncreaseFlighitsCarriedOut();
            airplane.SetNextFlight(@event.FlightStart);
            airplane.SetFlightEnd(@event.FlightEnd);
            airplane.SetAirplaneStatus(Domain.AirplaneStatus.Ready);

            await _airplanesRepository.UpdateAirplaneAsync(airplane);
            await _busPublisher.PublishAsync(new AirplaneReserved(@event.Id, @event.FlightStart ,@event.FlightEnd), context);
        }
    }
}