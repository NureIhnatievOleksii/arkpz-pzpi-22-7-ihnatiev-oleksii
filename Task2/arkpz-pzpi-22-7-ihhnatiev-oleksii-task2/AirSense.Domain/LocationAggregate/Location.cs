using AirSense.Domain.UserAggregate;

namespace AirSense.Domain.LocationAggregate;

public class Location
{
    public Guid LocationId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}
