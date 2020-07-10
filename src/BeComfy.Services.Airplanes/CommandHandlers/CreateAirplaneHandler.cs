using System;
using System.Threading.Tasks;
using BeComfy.Common.CqrsFlow.Handlers;
using BeComfy.Common.RabbitMq;
using BeComfy.Common.Types.Exceptions;
using BeComfy.Services.Airplanes.Domain;
using BeComfy.Services.Airplanes.Messages.Commands;
using BeComfy.Services.Airplanes.Messages.Events;
using BeComfy.Services.Airplanes.Repositories;

namespace BeComfy.Services.Airplanes.CommandHandlers
{
    public class CreateAirplaneHandler : ICommandHandler<CreateAirplane>
    {
        private readonly IAirplanesRepository _airplanesRepository;
        private readonly IBusPublisher _busPublisher;

        public CreateAirplaneHandler(IAirplanesRepository airplanesRepository, IBusPublisher busPublisher)
        {
            _airplanesRepository = airplanesRepository;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CreateAirplane command, ICorrelationContext context)
        {
            if (await _airplanesRepository.GetAsync(command.AirplaneRegistrationNumber) != null)
            {
                throw new BeComfyException("airplane_already_exists",
                    $"Airplane with registration number: '{command.AirplaneRegistrationNumber}' already exists.");
            }

            var airplane = new Airplane(command.AirplaneId, command.AirplaneRegistrationNumber, command.AirplaneModel, command.AvailableSeats,
                command.RequiredCrew);

            await _airplanesRepository.AddAsync(airplane);
            await _busPublisher.PublishAsync(new AirplaneCreated(command.AirplaneId, command.AirplaneRegistrationNumber, command.AirplaneModel), context);
        }
    }
}