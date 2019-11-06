using System;
using System.Threading.Tasks;
using BeComfy.Common.CqrsFlow.Handlers;
using BeComfy.Common.RabbitMq;
using BeComfy.Common.Types.Exceptions;
using BeComfy.Services.Airplanes.Messages.Commands;
using BeComfy.Services.Airplanes.Messages.Events;
using BeComfy.Services.Airplanes.Repositories;

namespace BeComfy.Services.Airplanes.CommandHandlers
{
    public class ReserveAirplaneHandler : ICommandHandler<ReserveAirplane>
    {
        private readonly IAirplanesRepository _airplanesRepository;
        private readonly IBusPublisher _busPublisher;

        public ReserveAirplaneHandler(IAirplanesRepository airplanesRepository, IBusPublisher busPublisher)
        {
            _airplanesRepository = airplanesRepository;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(ReserveAirplane command, ICorrelationContext context)
        {
            var airplane = await _airplanesRepository.GetAirplaneAsync(command.Id);

            if (airplane is null)
            {
                throw new BeComfyException($"Airplane with id '{command.Id}' does not exist");
            }

            airplane.SetNextFlight(command.FlightStart);
            airplane.SetFlightEnd(command.FlightEnd);
            airplane.SetAirplaneStatus(Domain.AirplaneStatus.Ready);

            await _airplanesRepository.UpdateAirplaneAsync(airplane);
            await _busPublisher.PublishAsync(new AirplaneReserved(command.Id, command.FlightStart, command.FlightEnd), context);
        }
    }
}