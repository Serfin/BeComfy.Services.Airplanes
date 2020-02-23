using System.Collections.Generic;
using BeComfy.Common.CqrsFlow;
using BeComfy.Common.Types.Enums;
using BeComfy.Services.Airplanes.Dto;

namespace BeComfy.Services.Airplanes.Queries
{
    public class BrowseAirplanes : IQuery<IEnumerable<AirplaneDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public AirplaneStatus Status { get; set; }
    }
}