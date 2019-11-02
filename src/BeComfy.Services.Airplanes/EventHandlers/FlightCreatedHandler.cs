using System.Threading.Tasks;
using BeComfy.Common.CqrsFlow.Handlers;
using BeComfy.Common.RabbitMq;
using BeComfy.Services.Airplanes.Messages.Events;
using BeComfy.Services.Airplanes.Repositories;

namespace BeComfy.Services.Airplanes.EventHandlers
{
    public class FlightCreatedHandler : IEventHandler<FlightCreated>
    {
        private readonly IAirplanesRepository _repository;

        public FlightCreatedHandler(IAirplanesRepository repository)
        {
            _repository = repository;
        }
        public async Task HandleAsync(FlightCreated @event, ICorrelationContext context)
        {
            var airplane = await _repository.GetAirplaneAsync(@event.Id);
            airplane.IncreaseFlighitsCarriedOut();
            await _repository.UpdateAirplaneAsync(airplane);
        }
    }
}