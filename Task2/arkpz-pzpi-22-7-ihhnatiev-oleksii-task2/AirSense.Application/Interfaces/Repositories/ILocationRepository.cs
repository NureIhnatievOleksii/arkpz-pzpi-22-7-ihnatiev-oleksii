using AirSense.Domain.LocationAggregate;

namespace AirSense.Application.Interfaces.Repositories;

public interface ILocationRepository
{
    Task CreateAsync(Location location, CancellationToken cancellationToken);
    Task<Location> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
