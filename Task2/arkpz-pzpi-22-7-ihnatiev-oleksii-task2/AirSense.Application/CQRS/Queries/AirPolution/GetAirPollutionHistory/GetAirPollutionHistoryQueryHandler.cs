using AirSense.Application.Interfaces.Repositories;
using MediatR;

namespace AirSense.Application.CQRS.Queries.AirPolution.GetAirPollutionHistory
{
    public class GetAirPollutionHistoryQueryHandler : IRequestHandler<GetAirPollutionHistoryQuery, string>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocationRepository _locationRepository;
        private readonly string _apiKey = "f17200f694647c61ef5127635704186f";

        public GetAirPollutionHistoryQueryHandler(
            IHttpClientFactory httpClientFactory,
            ILocationRepository locationRepository)
        {
            _httpClientFactory = httpClientFactory;
            _locationRepository = locationRepository;
        }

        public async Task<string> Handle(GetAirPollutionHistoryQuery request, CancellationToken cancellationToken)
        {
            // Получение локации из базы данных
            var location = await _locationRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            if (location == null)
            {
                throw new Exception("Location not found for the specified user.");
            }

            // Формирование запроса к API
            var client = _httpClientFactory.CreateClient();
            var url = $"http://api.openweathermap.org/data/2.5/air_pollution/history?lat={location.Latitude}&lon={location.Longitude}&start={request.Start}&end={request.End}&appid={_apiKey}";
            return await client.GetStringAsync(url);
        }
    }
}
