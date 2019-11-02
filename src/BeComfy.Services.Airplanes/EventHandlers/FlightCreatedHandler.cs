using System.Threading.Tasks;
using BeComfy.Common.CqrsFlow.Handlers;
using BeComfy.Common.RabbitMq;
using BeComfy.Services.Airplanes.Messages.Events;

namespace BeComfy.Services.Airplanes.EventHandlers
{
    public class FlightCreatedHandler : IEventHandler<FlightCreated>
    {
        public Task HandleAsync(FlightCreated @event, ICorrelationContext context)
        {
            //throw new System.NotImplementedException("Handled");
            return Task.CompletedTask;
        }
    }
}