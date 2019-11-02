using System;
using BeComfy.Common.CqrsFlow;
using BeComfy.Services.Airplanes.Dto;

namespace BeComfy.Services.Airplanes.Queries
{
    public class GetAirplane : IQuery<AirplaneDto>
    {
        public Guid Id { get; set; }
    }
}