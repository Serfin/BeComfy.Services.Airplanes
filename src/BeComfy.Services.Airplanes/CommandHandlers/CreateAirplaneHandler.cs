using System;
using System.Threading.Tasks;
using BeComfy.Common.CqrsFlow.Handlers;
using BeComfy.Common.RabbitMq;
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
            var airplane = new Airplane(command.Id, command.AvailableSeats);

            await _airplanesRepository.AddAsync(airplane);
            await _busPublisher.PublishAsync(new AirplaneCreated(command.Id, command.Model), context);
        }
    }
}