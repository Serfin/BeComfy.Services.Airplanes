using System.Collections.Generic;
using System.Threading.Tasks;
using BeComfy.Common.CqrsFlow.Dispatcher;
using BeComfy.Services.Airplanes.Dto;
using BeComfy.Services.Airplanes.Queries;
using Microsoft.AspNetCore.Mvc;

namespace BeComfy.Services.Airplanes.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AirplanesController : BaseController
    {
        public AirplanesController(IQueryDispatcher queryDispatcher)
            : base(queryDispatcher)
        {

        }
            
        [HttpGet("{id}")]
        public async Task<ActionResult<AirplaneDto>> GetAsync([FromRoute] GetAirplane query)
            => Ok(await QueryAsync(query));

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AirplaneDto>>> BrowseAsync([FromQuery] BrowseAirplanes query)
            => Ok(await QueryAsync(query));
    }
}
