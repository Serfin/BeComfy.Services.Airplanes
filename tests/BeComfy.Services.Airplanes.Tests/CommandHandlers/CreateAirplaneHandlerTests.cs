using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeComfy.Common.CqrsFlow;
using BeComfy.Common.RabbitMq;
using BeComfy.Common.Types.Enums;
using BeComfy.Common.Types.Exceptions;
using BeComfy.Services.Airplanes.CommandHandlers;
using BeComfy.Services.Airplanes.Domain;
using BeComfy.Services.Airplanes.Messages.Commands;
using BeComfy.Services.Airplanes.Messages.Events;
using BeComfy.Services.Airplanes.Repositories;
using NSubstitute;
using NUnit.Framework;

namespace BeComfy.Services.Airplanes.Tests.CommandHandlers
{
    [TestFixture]
    public class CreateAirplaneHandlerTests
    {
        #region Arrange

        private readonly IAirplanesRepository _airplanesRepository;
        private readonly IBusPublisher _busPublisher;
        private readonly IBusSubscriber _busSubscriber;
        private readonly ICorrelationContext _correlationContext;
        private readonly CreateAirplaneHandler _commandHandler;

        public CreateAirplaneHandlerTests()
        {
            _airplanesRepository = Substitute.For<IAirplanesRepository>();
            _busPublisher = Substitute.For<IBusPublisher>();
            _busSubscriber = Substitute.For<IBusSubscriber>();
            _correlationContext = Substitute.For<ICorrelationContext>();
            _commandHandler = new CreateAirplaneHandler(_airplanesRepository, _busPublisher);
        }

        private Guid _id = Guid.Parse("88161a61-8924-48f6-85f6-120b5a29338c");
        private string _airplaneRegistraionNumber = "randomRegistrationNumber";
        private string _model = "model";
        private IDictionary<SeatClass, int> _availableSeats = new Dictionary<SeatClass, int>
        {
            { SeatClass.Business, 15 }
        };

        private CreateAirplane _command => new CreateAirplane(_id, _airplaneRegistraionNumber, _model, _availableSeats);

        private async Task Ack(CreateAirplane command)
            => await _commandHandler.HandleAsync(command, _correlationContext);

        #endregion

        #region Act/Assert

        [Test]
        public void empty_airplane_model_throws_domain_validation_exception()
        {
            Assert.Throws<BeComfyDomainException>(() => _airplanesRepository.AddAsync(new Airplane(Guid.NewGuid(), 
                _airplaneRegistraionNumber, "", _availableSeats)));
        }

        [Test]
        public async Task failed_to_create_airplane_published_create_airplane_rejected()
        {
            _airplanesRepository.GetAsync(_airplaneRegistraionNumber)
                .Returns(new Airplane(_id, _airplaneRegistraionNumber, _model, _availableSeats));

            try 
            {
                await Ack(_command);
            }
            catch (Exception ex)
            {
                if (ex is BeComfyException)
                {
                    await _busPublisher.PublishAsync(new CreateAirplaneRejected(_command.Id, _command.AirplaneRegistrationNumber,
                        _command.Model, "airplane_already_exists", $"Airplane with registration number: '{_command.AirplaneRegistrationNumber}' already exists."
                        ), _correlationContext);
                }
            }

            await _busPublisher
                .Received()
                .PublishAsync(Arg.Is<CreateAirplaneRejected>(e =>
                    e.Id == _command.Id
                    && e.AirplaneRegistrationNumber == _command.AirplaneRegistrationNumber
                    && e.Code == "airplane_already_exists"
                    && e.Reason == $"Airplane with registration number: '{_command.AirplaneRegistrationNumber}' already exists."
                        ), _correlationContext);
        }

        #endregion
    }
}