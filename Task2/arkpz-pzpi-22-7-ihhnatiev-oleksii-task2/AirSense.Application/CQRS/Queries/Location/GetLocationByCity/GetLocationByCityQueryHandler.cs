using MediatR;

namespace AirSense.Application.CQRS.Queries.Location.GetLocationByCity
{
    public class GetLocationByCityQueryHandler : IRequestHandler<GetLocationByCityQuery, string>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey = "f17200f694647c61ef5127635704186f";

        public GetLocationByCityQueryHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> Handle(GetLocationByCityQuery request, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient();
            var url = $"http://api.openweathermap.org/geo/1.0/direct?q={request.City},{request.State},{request.Country}&limit={request.Limit}&appid={_apiKey}";
            return await client.GetStringAsync(url);
        }
    }
}
