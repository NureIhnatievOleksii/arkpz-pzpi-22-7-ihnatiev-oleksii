using AirSense.Application.CQRS.Commands.Locations.CreateLocation;
using AirSense.Application.CQRS.Queries.Location.GetLocationByCity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace AirSense.Api.Controllers
{
    [Route("api/location")]
    public class LocationController(IMediator mediator, IHttpClientFactory httpClientFactory) : ControllerBase
    {
        
        [HttpPost]
        [Authorize(Roles = "Client,Admin")]
        public async Task<IActionResult> CreateLocation([FromBody] CreateLocationCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetLocationByCity(string city, string state, string country, int limit, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetLocationByCityQuery(city, state, country, limit), cancellationToken);
            return string.IsNullOrEmpty(result) ? BadRequest("No data found for the given city.") : Ok(result);
        }
    }
}
