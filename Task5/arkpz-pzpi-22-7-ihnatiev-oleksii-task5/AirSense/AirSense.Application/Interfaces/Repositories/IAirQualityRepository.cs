using AirSense.Domain.AirQualityAggregate;

namespace AirSense.Application.Interfaces.Repositories
{
    public interface IAirQualityRepository
    {
        Task AddAirQualityAsync(AirQuality airQuality);
    }
}