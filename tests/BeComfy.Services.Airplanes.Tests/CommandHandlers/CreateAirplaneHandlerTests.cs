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

        private IDictionary<EmployeePosition, int> _requiredCrew = new Dictionary<EmployeePosition, int>
        {
            { EmployeePosition.Pilot, 2 },
            { EmployeePosition.Staff, 15 }
        };

        private CreateAirplane _command => new CreateAirplane(_id, _airplaneRegistraionNumber, _model, _availableSeats, _requiredCrew);

        #endregion

        #region Act/Assert

        [Test]
        public void empty_airplane_model_throws_domain_validation_exception()
        {
            Assert.Throws<BeComfyException>(() => _airplanesRepository.AddAsync(new Airplane(Guid.NewGuid(), 
                _airplaneRegistraionNumber, "", _availableSeats, _requiredCrew)), "airplane_empty_id", "Airplane id cannot be empty");
        }

        [Test]
        public async Task create_airplane_with_existing_registration_number_failed_published_create_airplane_rejected()
        {
            _airplanesRepository.GetAsync(_airplaneRegistraionNumber)
                .Returns(new Airplane(_id, _airplaneRegistraionNumber, _model, _availableSeats, _requiredCrew));

            try
            {
                await _commandHandler.HandleAsync(_command, _correlationContext);
            }
            catch (Exception ex)
            {
                if (ex is BeComfyException)
                {
                    await _busPublisher.PublishAsync(new CreateAirplaneRejected(_command.AirplaneId, 
                        _command.AirplaneRegistrationNumber, _command.AirplaneModel, "airplane_already_exists", 
                        $"Airplane with registration number: '{_command.AirplaneRegistrationNumber}' already exists."
                        ), _correlationContext);
                }
            }

            await _busPublisher
                .Received()
                .PublishAsync(Arg.Is<CreateAirplaneRejected>(e =>
                    e.Id == _command.AirplaneId
                    && e.AirplaneRegistrationNumber == _command.AirplaneRegistrationNumber
                    && e.Code == "airplane_already_exists"
                    && e.Reason == $"Airplane with registration number: '{_command.AirplaneRegistrationNumber}' already exists."
                        ), _correlationContext);

            await _airplanesRepository.DeleteAsync(_id);
        }

        [Test]
        public async Task create_airplane_on_success_published_airplane_created()
        {
            _airplanesRepository.GetAsync(_airplaneRegistraionNumber).Returns<Airplane>((Airplane) null);
              
            await _commandHandler.HandleAsync(_command, _correlationContext);

            await _busPublisher
                .Received()
                .PublishAsync(Arg.Is<AirplaneCreated>(e =>
                    e.Id == _command.AirplaneId && 
                    e.AirplaneRegistrationNumber == _command.AirplaneRegistrationNumber && 
                    e.Model == _command.AirplaneModel), _correlationContext);
        }

        #endregion
    }
}