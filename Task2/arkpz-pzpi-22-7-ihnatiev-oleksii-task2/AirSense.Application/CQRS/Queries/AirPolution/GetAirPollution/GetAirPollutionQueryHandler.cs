using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AirSense.Application.Interfaces.Repositories;

namespace AirSense.Application.CQRS.Queries.AirPolution.GetAirPollution
{
    public class GetAirPollutionQueryHandler : IRequestHandler<GetAirPollutionQuery, string>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocationRepository _locationRepository;
        private readonly string _apiKey = "f17200f694647c61ef5127635704186f";

        public GetAirPollutionQueryHandler(
            IHttpClientFactory httpClientFactory,
            ILocationRepository locationRepository)
        {
            _httpClientFactory = httpClientFactory;
            _locationRepository = locationRepository;
        }

        public async Task<string> Handle(GetAirPollutionQuery request, CancellationToken cancellationToken)
        {
            // Получение локации из базы данных
            var location = await _locationRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            if (location == null)
            {
                throw new Exception("Location not found for the specified user.");
            }

            // Формирование запроса к API
            var client = _httpClientFactory.CreateClient();
            var url = $"http://api.openweathermap.org/data/2.5/air_pollution?lat={location.Latitude}&lon={location.Longitude}&appid={_apiKey}";
            return await client.GetStringAsync(url);
        }
    }
}
