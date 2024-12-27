
    using AirSense.Application.Interfaces.Repositories;
    using AirSense.Domain.AirQualityAggregate;
    using AirSense.Infrastructure.Database;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class AirQualityRepository : IAirQualityRepository
    {
        private readonly AirSenseContext _context;

        public AirQualityRepository(AirSenseContext context)
        {
            _context = context;
        }

        public async Task AddAirQualityAsync(AirQuality airQuality)
        {
            await _context.AirQualities.AddAsync(airQuality);
            await _context.SaveChangesAsync();
        }
    }
