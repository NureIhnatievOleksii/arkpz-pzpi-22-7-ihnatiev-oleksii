using AirSense.Domain.LocationAggregate;
using Microsoft.AspNetCore.Identity;

namespace AirSense.Domain.UserAggregate;

public class User : IdentityUser<Guid>
{
    public string? Photo { get; set; }
    public bool IsBanned { get; set; } = false;
    public ICollection<Location> Locations { get; set; } = new List<Location>();
}
