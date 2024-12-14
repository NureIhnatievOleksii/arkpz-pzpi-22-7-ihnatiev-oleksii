using AirSense.Application.CQRS.Queries.AirPolution.GetAirPollution;
using AirSense.Application.CQRS.Queries.AirPolution.GetAirPollutionHistory;
using AirSense.Application.CQRS.Queries.Location.GetLocationByCity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirSense.Api.Controllers
{
    [Route("api/air-pollution/")]
    public class AirPollutionController(IMediator mediator, IHttpClientFactory httpClientFactory) : ControllerBase
    {
        [HttpGet("{userId}")]
        [Authorize(Roles = "Client,Admin")]
        public async Task<IActionResult> GetAirPollution(Guid userId, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAirPollutionQuery(userId), cancellationToken);

            return string.IsNullOrEmpty(result)
                ? NotFound("No air pollution data found for the given user.")
                : Ok(result);
        }



        [HttpGet("history")]
        [Authorize(Roles = "Client,Admin")]
        public async Task<IActionResult> GetAirPollutionHistory(
    Guid userId,
    DateTime start,
    DateTime end,
    CancellationToken cancellationToken)
        {
            // Преобразуем обычные даты в Unix timestamp
            long startUnix = ToUnixTimestamp(start);
            long endUnix = ToUnixTimestamp(end);

            var result = await mediator.Send(new GetAirPollutionHistoryQuery(userId, startUnix, endUnix), cancellationToken);

            return string.IsNullOrEmpty(result)
                ? BadRequest("No data found for the given user and time range.")
                : Ok(result);
        }

        public static long ToUnixTimestamp(DateTime dateTime)
        {
            DateTimeOffset dto = new DateTimeOffset(dateTime);
            return dto.ToUnixTimeSeconds();
        }


    }
}
