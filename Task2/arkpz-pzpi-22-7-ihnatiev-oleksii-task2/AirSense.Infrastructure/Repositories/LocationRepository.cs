using Microsoft.EntityFrameworkCore;
using AirSense.Application.Interfaces.Repositories;
using AirSense.Domain.LocationAggregate;
using AirSense.Infrastructure.Database;

namespace AirSense.Infrastructure.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly AirSenseContext _context;

    public LocationRepository(AirSenseContext context)
    {
        _context = context;
    }

    
    public async Task CreateAsync(Location location, CancellationToken cancellationToken = default)
    {
        await _context.Locations.AddAsync(location, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<Location> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Locations
            .Include(l => l.User)
            .FirstOrDefaultAsync(l => l.UserId == userId, cancellationToken);
    }
}
