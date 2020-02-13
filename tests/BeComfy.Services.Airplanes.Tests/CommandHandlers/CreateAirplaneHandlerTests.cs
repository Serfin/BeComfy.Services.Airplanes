using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeComfy.Common.RabbitMq;
using BeComfy.Common.Types.Enums;
using BeComfy.Common.Types.Exceptions;
using BeComfy.Services.Airplanes.CommandHandlers;
using BeComfy.Services.Airplanes.Domain;
using BeComfy.Services.Airplanes.Messages.Commands;
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
        private readonly ICorrelationContext _correlationContext;
        private readonly CreateAirplaneHandler _commandHandler;

        public CreateAirplaneHandlerTests()
        {
            _airplanesRepository = Substitute.For<IAirplanesRepository>();
            _busPublisher = Substitute.For<IBusPublisher>();
            _correlationContext = Substitute.For<ICorrelationContext>();
            _commandHandler = new CreateAirplaneHandler(_airplanesRepository, _busPublisher);
        }

        private Guid _id = Guid.Parse("88161a61-8924-48f6-85f6-120b5a29338c");
        private string _model = "model";
        private IDictionary<SeatClass, int> _availableSeats = new Dictionary<SeatClass, int>
        {
            { SeatClass.Business, 15 }
        };

        private CreateAirplane _command => new CreateAirplane(_id, _model, _availableSeats);

        private async Task Ack(CreateAirplane command)
            => await _commandHandler.HandleAsync(command, _correlationContext);

        #endregion

        [Test]
        public async Task failed_to_create_airplane_published_create_airplane_rejected_event()
        {
            //_airplanesRepository.GetAsync(_id).Returns(new Airplane(_id, _model, _availableSeats));

            Assert.Throws<BeComfyDomainException>(() => _airplanesRepository.AddAsync(new Airplane(Guid.NewGuid(), "", _availableSeats)));
        }
    }
}